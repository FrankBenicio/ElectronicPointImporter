using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class DepartmentPayment
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("Departamento")]
        public string Department { get; set; }
        [JsonPropertyName("MesVigencia")]
        public string MonthTerm { get; set; }
        [JsonPropertyName("AnoVigencia")]
        public int YearTerm { get; set; }
        [JsonPropertyName("TotalPagar")]
        public decimal TotalPay { get; set; }
        [JsonPropertyName("TotalDescontos")]
        public decimal TotalDiscounts { get; set; }
        [JsonPropertyName("TotalExtras")]
        public decimal TotalExtras { get; set; }
        [JsonIgnore]
        public Guid ArchiveId { get; set; }
        [JsonPropertyName("Funcionarios")]
        public List<Employee> Employees { get; set; } = new ();

    }
}
