using AutoFixture;
using Data.Tests.Context;
using Data.UseCases;
using Domain.Models;
using Domain.Requests;

namespace Data.Tests.UseCases
{
    public class GetArchiveUseCaseTests
    {
        [Fact]
        public async Task ShouldReturnArchive()
        {
            var mockDatabase = await ContextFake.Gerar(Guid.NewGuid().ToString());

            var getArchive = new GetArchiveUseCase(mockDatabase);

            var fixture = new Fixture();

            var archiveFixure = fixture.Create<Archive>();

            await mockDatabase.AddAsync(archiveFixure);
            await mockDatabase.SaveChangesAsync();

            var request = new ArchiveIdRequest
            {
                Id = archiveFixure.Id
            };

            var archive = await getArchive.Execute(request);

            Assert.Equal(archiveFixure.Id, archive.Id);
            Assert.Equal(archiveFixure.Name, archive.Name);
            Assert.Equal(archiveFixure.Directory, archive.Directory);
        }
    }
}
