using AutoFixture;
using Data.Tests.Context;
using Data.UseCases;
using Domain.Models;
using Domain.Requests;

namespace Data.Tests.UseCases
{
    public class ProcessArchiveUseCaseTests
    {
        [Fact]
        public async Task ShouldReturnArchiveProcessed()
        {
            var mockDatabase = await ContextFake.Gerar(Guid.NewGuid().ToString());

            var processArchive = new ProcessArchiveUseCase(mockDatabase);

            var fixture = new Fixture();

            var archiveFixure = fixture.Create<Archive>();

            await mockDatabase.AddAsync(archiveFixure);
            await mockDatabase.SaveChangesAsync();

            var request = new ArchiveIdRequest
            {
                Id = archiveFixure.Id
            };

            await processArchive.Execute(request);

            var archive = mockDatabase.Archives.First();
            Assert.Equal(Domain.Enums.ProcessingStatus.PROCESSING, archive.Status);
        }
    }
}
