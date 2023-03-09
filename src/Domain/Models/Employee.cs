using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Domain.Models
{
    public class Employee
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        [JsonPropertyName("Codigo")]
        public int Code { get; set; }

        [JsonPropertyName("TotalReceber")]
        public decimal TotalReceive { get; set; }

        [JsonPropertyName("HorasExtras")]
        public double Overtime { get; set; }

        [JsonPropertyName("HorasDebito")]
        public double DebitHours { get; set; }

        [JsonPropertyName("DiasFalta")]
        public double MissingDays { get; set; }

        [JsonPropertyName("DiasExtras")]
        public double ExtraDays { get; set; }

        [JsonPropertyName("DiasTrabalhados")]
        public double WorkedDays { get; set; }

    }
}
