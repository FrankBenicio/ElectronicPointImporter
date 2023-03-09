using System.Diagnostics;
using System.Xml.Linq;

namespace Processor.Models
{
    public class Employee
    {
        public Employee(string name, int code, decimal totalReceive, double workedDays)
        {
            Name = name;
            Code = code;
            TotalReceive = totalReceive;
            WorkedDays = workedDays;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public int Code { get; private set; }
        public decimal TotalReceive { get; private set; }
        public double Overtime { get; private set; }
        public double DebitHours { get; private set; }
        public double MissingDays { get; private set; }
        public double ExtraDays { get; private set; }
        public double WorkedDays { get; private set; }

        public void SetDebitAndExtrasSchedule(double debitHours, double overtime)
        {
            DebitHours = debitHours;
            Overtime = overtime;
        }
        public void DefineMissingDaysAndExtraDays(double extraDays, double missingDays)
        {
            ExtraDays = extraDays;
            MissingDays = missingDays;
        }
    }
}
