using CsvHelper.Configuration.Attributes;

namespace Processor.Models
{
    public class PointCsv
    {
        [Name("Código")]
        public string Code { get; set; }

        [Name("Nome")]
        public string Name { get; set; }

        [Name("Valor hora")]
        public string HourValue { get; set; }

        [Name("Data")]
        public string Date { get; set; }

        [Name("Entrada")]
        public string Input { get; set; }

        [Name("Saída")]
        public string Output { get; set; }

        [Name("Almoço")]
        public string Lunch { get; set; }
    }
}
