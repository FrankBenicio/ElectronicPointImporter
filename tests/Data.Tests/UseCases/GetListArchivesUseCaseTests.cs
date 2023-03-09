using AutoFixture;
using Data.Tests.Context;
using Data.UseCases;
using Domain.Models;
using Domain.Requests;

namespace Data.Tests.UseCases
{
    public class GetListArchivesUseCaseTests
    {
        [Fact]
        public async Task ShouldReturnArchives()
        {
            var mockDatabase = await ContextFake.Gerar(Guid.NewGuid().ToString());

            var getAllArchive = new GetListArchivesUseCase(mockDatabase);

            var fixture = new Fixture();

            var archivesFixure = fixture.CreateMany<Archive>();

            await mockDatabase.AddRangeAsync(archivesFixure);
            await mockDatabase.SaveChangesAsync();


            var archives = await getAllArchive.Execute();

            Assert.Equal(archivesFixure.Count(), archives.Count);
        }
    }
}
