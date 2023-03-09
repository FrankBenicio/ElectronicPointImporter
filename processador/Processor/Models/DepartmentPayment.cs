namespace Processor.Models
{
    public class DepartmentPayment
    {
        public DepartmentPayment(string department, string monthTerm, int yearTerm, Guid archiveId)
        {
            Department = department;
            MonthTerm = monthTerm;
            YearTerm = yearTerm;
            ArchiveId = archiveId;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Department { get; private set; }
        public string MonthTerm { get; private set; }
        public int YearTerm { get; private set; }
        public decimal TotalPay { get; private set; }
        public decimal TotalDiscounts { get; private set; }
        public decimal TotalExtras { get; private set; }
        public Guid ArchiveId { get; private set; }
        public List<Employee> Employees { get; private set; } = new ();

        public void AddEmployees(List<Employee> employees)
        {
            Employees.AddRange(employees);
        }
        public void DefinePaidAmounts(decimal totalPay, decimal totalDiscounts, decimal totalExtras)
        {
            TotalDiscounts= totalDiscounts;
            TotalExtras= totalExtras;
            TotalPay = totalPay;
        }
    }
}
