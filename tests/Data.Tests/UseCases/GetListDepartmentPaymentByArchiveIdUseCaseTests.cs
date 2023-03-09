using AutoFixture;
using Data.Tests.Context;
using Data.UseCases;
using Domain.Models;
using Domain.Requests;

namespace Data.Tests.UseCases
{
    public class GetListDepartmentPaymentByArchiveIdUseCaseTests
    {
        [Fact]
        public async Task ShouldReturnDepartmentsPayments()
        {
            var mockDatabase = await ContextFake.Gerar(Guid.NewGuid().ToString());

            var getArchive = new GetListDepartmentPaymentByArchiveIdUseCase(mockDatabase);

            var fixture = new Fixture();

            var archiveFixure = fixture.Create<Archive>();
            var departmentPaymentsFixture = fixture.CreateMany<DepartmentPayment>();

            departmentPaymentsFixture.ToList().ForEach(x => x.ArchiveId = archiveFixure.Id);

            await mockDatabase.AddAsync(archiveFixure);
            await mockDatabase.AddRangeAsync(departmentPaymentsFixture);
            await mockDatabase.SaveChangesAsync();

            var request = new ArchiveIdRequest
            {
                Id = archiveFixure.Id
            };

            var departmentPayments = await getArchive.Execute(request);

            Assert.Equal(departmentPaymentsFixture.Count(), departmentPayments.Count);
        }
    }
}
