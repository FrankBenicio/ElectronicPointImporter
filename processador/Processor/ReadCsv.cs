using CsvHelper;
using System.Globalization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Processor.Models;
using Processor.Services.BlobStorage;
using Processor.Services.Refit;
using System.Collections;
using System.Text;
using CsvHelper.Configuration;

namespace Processor
{
    public class ReadCsv
    {
        private readonly ILogger _logger;
        private readonly IArchiveApi archiveApi;
        private readonly IDepartmentPaymentApi departmentPaymentApi;
        private readonly BlobStorageService blobStorageService;
        public ReadCsv(ILoggerFactory loggerFactory, IArchiveApi archiveApi, BlobStorageService blobStorageService, IDepartmentPaymentApi departmentPaymentApi)
        {
            _logger = loggerFactory.CreateLogger<ReadCsv>();
            this.archiveApi = archiveApi;
            this.blobStorageService = blobStorageService;
            this.departmentPaymentApi = departmentPaymentApi;
        }

        [Function("read-archive-csv")]
        public async Task Run([ServiceBusTrigger("sbq-archive-created.file", Connection = "MyServiceBus")] ArchiveCreatedRequest message)
        {
            _logger.LogInformation($"Get Archive");
            var archive = await archiveApi.GetArchive(message.Id);
            _logger.LogInformation($"Archive {archive.Name}");

            var departamentInfo = archive.Name.Split("-");

            if (departamentInfo.Count() != 3)
                return;

            _logger.LogInformation($"Update Status Archive");
            await archiveApi.ProcessArchive(message.Id);
            _logger.LogInformation($"Updated Status Archive");

            _logger.LogInformation($"Get Archive Csv");
            var archiveBlob = await blobStorageService.GetAsync(archive.Directory);


            _logger.LogInformation($"Convert Archive Csv to Object");
            var pointCsv = new List<PointCsv>();
            using (var stream = new MemoryStream(archiveBlob))
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    pointCsv = csv.GetRecords<PointCsv>().ToList();
                }
            }

            if (!pointCsv.Any())
                return;

            _logger.LogInformation($"Calculate payment amounts");

            var deparmantePayment = new DepartmentPayment(department: departamentInfo[0], monthTerm: departamentInfo[1], yearTerm: int.Parse(departamentInfo[2].Replace(".csv", "")), archiveId: archive.Id);

            var employees = pointCsv.GroupBy(x => x.Code)
                .Select(x =>
                {
                    var employee = new Employee(
                        name: x.First().Name,
                        code: int.Parse(x.Key),
                        totalReceive: x.Sum(s => decimal.Parse(s.HourValue.Replace("R$", ""))),
                        workedDays: x.Count());
                    int extraDays, missingDays;
                    double overtime, debitHours;
                    CalculateOtherInformation(x, out extraDays, out missingDays, out overtime, out debitHours);

                    employee.DefineMissingDaysAndExtraDays(extraDays, missingDays);
                    employee.SetDebitAndExtrasSchedule(debitHours, overtime);

                    return employee;

                }).ToList();

            var departamentTotalPay = (decimal)pointCsv.Sum(x => double.Parse(x.HourValue.Replace("R$", "")));
            var totalDiscounts = (decimal)employees.Sum(x => x.DebitHours);
            var totalExtras = (decimal)employees.Sum(x => x.Overtime);
            deparmantePayment.AddEmployees(employees);
            deparmantePayment.DefinePaidAmounts(totalPay: departamentTotalPay, totalDiscounts: totalDiscounts, totalExtras: totalExtras);

            _logger.LogInformation($"Save deparmantePayment");
            await departmentPaymentApi.Post(deparmantePayment);
            _logger.LogInformation($"Saved deparmantePayment");

            _logger.LogInformation($"Update Status Archive");
            await archiveApi.FinalizeArchive(message.Id);
            _logger.LogInformation($"Updated Status Archive");


        }

        private static void CalculateOtherInformation(IGrouping<string, PointCsv> x, out int extraDays, out int missingDays, out double overtime, out double debitHours)
        {
            extraDays = 0;
            missingDays = 0;
            overtime = 0d;
            debitHours = 0d;
            if (x.Count() > 30)
            {
                extraDays = x.Count() - 30;
                overtime = double.Parse(x.First().HourValue.Replace("R$", "")) * extraDays;
            }
            else if (x.Count() < 30)
            {
                missingDays = 30 - x.Count();
                debitHours = double.Parse(x.First().HourValue.Replace("R$", "")) * missingDays;
            }
        }
    }
}
