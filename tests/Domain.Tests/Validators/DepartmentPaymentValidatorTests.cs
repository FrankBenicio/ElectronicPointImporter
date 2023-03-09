using Domain.Models;
using Domain.Requests;
using Domain.Validators;
using FluentValidation.TestHelper;

namespace Domain.Tests.Validators
{
    public class DepartmentPaymentValidatorTests
    {
        [Fact]
        public void ShouldHaveErrorWhenValuesIsEmpty()
        {
            var model = new DepartmentPayment
            {
                ArchiveId= Guid.Empty,
                Department = string.Empty,
                Employees = null,
                Id= Guid.Empty,
                MonthTerm = string.Empty,
                TotalDiscounts = -1m,
                TotalExtras= -1m,
                TotalPay = 0,
                YearTerm = 0                 
            };

            var validator = new DepartmentPaymentValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ArchiveId);
            result.ShouldHaveValidationErrorFor(x => x.Department);
            result.ShouldHaveValidationErrorFor(x => x.Employees);
            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.MonthTerm);
            result.ShouldHaveValidationErrorFor(x => x.TotalDiscounts);
            result.ShouldHaveValidationErrorFor(x => x.TotalExtras);
            result.ShouldHaveValidationErrorFor(x => x.TotalPay);
            result.ShouldHaveValidationErrorFor(x => x.YearTerm);
        }

        [Fact]
        public void ShouldHaveNotError()
        {
            var model = new DepartmentPayment
            {
                ArchiveId= Guid.NewGuid(),
                Department = "Department",
                Employees = new List<Employee>() { new Employee() { } },
                Id= Guid.NewGuid(),
                MonthTerm = "MonthTerm",
                TotalDiscounts = 0m,
                TotalExtras= 0m,
                TotalPay = 1000,
                YearTerm = 2023                 
            };

            var validator = new DepartmentPaymentValidator();

            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.ArchiveId);
            result.ShouldNotHaveValidationErrorFor(x => x.Department);
            result.ShouldNotHaveValidationErrorFor(x => x.Employees);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.MonthTerm);
            result.ShouldNotHaveValidationErrorFor(x => x.TotalDiscounts);
            result.ShouldNotHaveValidationErrorFor(x => x.TotalExtras);
            result.ShouldNotHaveValidationErrorFor(x => x.TotalPay);
            result.ShouldNotHaveValidationErrorFor(x => x.YearTerm);
        }
    }
}
