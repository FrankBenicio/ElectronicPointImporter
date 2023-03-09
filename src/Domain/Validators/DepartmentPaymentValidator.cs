using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class DepartmentPaymentValidator : AbstractValidator<DepartmentPayment>
    {
        public DepartmentPaymentValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.YearTerm).GreaterThan(0);
            RuleFor(x => x.TotalPay).GreaterThan(0);
            RuleFor(x => x.TotalDiscounts).Must(x => x >= 0);
            RuleFor(x => x.TotalExtras).Must(x => x >= 0);
            RuleFor(x => x.MonthTerm).NotNull().NotEmpty();
            RuleFor(x => x.Department).NotNull().NotEmpty();
            RuleFor(x => x.ArchiveId).NotNull().NotEmpty();
            RuleFor(x => x.Employees).NotNull().NotEmpty();
        }
    }
}
