
using Api.Controllers;
using AutoFixture;
using Domain.Interfaces;
using Domain.Models;
using Domain.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests
{
    public class DepartmentPaymentTests
    {
        [Fact]
        public async Task GetDepartmentPaymentShouldReturnOkObjectResult()
        {
            var validator = new InlineValidator<ArchiveIdRequest>();

            var mockUseCase = new Mock<IGetListDepartmentPaymentByArchiveIdUseCase>();

            var controller = new DepartmentPaymentController();

            var result = await controller.Get(validator, mockUseCase.Object, Guid.NewGuid());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetDepartmentPaymentShouldReturnBadRequestObjectResult()
        {
            var validator = new InlineValidator<ArchiveIdRequest>();
            validator.RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("ArchiveId not found");

            var mockUseCase = new Mock<IGetListDepartmentPaymentByArchiveIdUseCase>();

            var controller = new DepartmentPaymentController();

            var result = await controller.Get(validator, mockUseCase.Object, Guid.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetDepartmentPaymentShouldReturnBadRequestObjectResultWhenException()
        {
            var validator = new InlineValidator<ArchiveIdRequest>();
            validator.RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("ArchiveId not found");

            var mockUseCase = new Mock<IGetListDepartmentPaymentByArchiveIdUseCase>();
            mockUseCase.Setup(x => x.Execute(It.IsAny<ArchiveIdRequest>())).ThrowsAsync(new Exception());
            var controller = new DepartmentPaymentController();

            var result = await controller.Get(validator, mockUseCase.Object, Guid.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostDepartmentPaymentShouldReturnOkResult()
        {
            var validator = new InlineValidator<DepartmentPayment>();

            var mockUseCase = new Mock<ICreateDepartmentPaymentUseCase>();

            var controller = new DepartmentPaymentController();

            var departmentPayment = new Fixture().Create<DepartmentPayment>();

            var result = await controller.Post(validator, mockUseCase.Object, departmentPayment);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PostDepartmentPaymentShouldReturnBadRequestObjectResult()
        {
            var validator = new InlineValidator<DepartmentPayment>();
            validator.RuleFor(x => x.Id).NotNull().NotEmpty();
            validator.RuleFor(x => x.YearTerm).GreaterThan(0);
            validator.RuleFor(x => x.TotalPay).GreaterThan(0);
            validator.RuleFor(x => x.TotalDiscounts).Must(x => x >= 0);
            validator.RuleFor(x => x.TotalExtras).Must(x => x >= 0);
            validator.RuleFor(x => x.MonthTerm).NotNull().NotEmpty();
            validator.RuleFor(x => x.Department).NotNull().NotEmpty();
            validator.RuleFor(x => x.ArchiveId).NotNull().NotEmpty();
            validator.RuleFor(x => x.Employees).NotNull().NotEmpty();

            var mockUseCase = new Mock<ICreateDepartmentPaymentUseCase>();

            var controller = new DepartmentPaymentController();

            var departmentPayment = new DepartmentPayment();

            var result = await controller.Post(validator, mockUseCase.Object, departmentPayment);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostDepartmentPaymentShouldReturnBadRequestObjectResultWhenException()
        {
            var validator = new InlineValidator<DepartmentPayment>();

            var mockUseCase = new Mock<ICreateDepartmentPaymentUseCase>();
            mockUseCase.Setup(x => x.Execute(It.IsAny<DepartmentPayment>())).ThrowsAsync(new Exception());
            var controller = new DepartmentPaymentController();
            var departmentPayment = new DepartmentPayment();
            var result = await controller.Post(validator, mockUseCase.Object, departmentPayment);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
