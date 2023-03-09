using AutoFixture;
using Data.Tests.Context;
using Data.UseCases;
using Domain.Models;

namespace Data.Tests.UseCases
{
    public class CreateDepartmentPaymentUseCaseTests
    {
        [Fact]
        public async Task ShouldCreateDepartmentPayment()
        {
            var mockDatabase = await ContextFake.Gerar(Guid.NewGuid().ToString());

            var createDepartmentPayment = new CreateDepartmentPaymentUseCase(mockDatabase);

            var fixture = new Fixture();

            var deparmentPaymentFixture = fixture.Create<DepartmentPayment>();

            await createDepartmentPayment.Execute(deparmentPaymentFixture);

            var deparmentPayment = mockDatabase.DepartmentsPayment.First();

            Assert.NotNull(deparmentPayment);
        }
    }
}
