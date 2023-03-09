using AutoFixture;
using Azure.Messaging.ServiceBus;
using Data.Tests.Context;
using Data.UseCases;
using Domain.BlobStorage;
using Domain.Events;
using Domain.Models;
using Domain.Requests;
using Infra.Context;
using Infra.Events;
using Moq;

namespace Data.Tests.UseCases
{
    public class CreateArchiveUseCaseTests
    {
        [Fact]
        public async Task ShouldCreateArchive()
        {
            var mockDatabase = await ContextFake.Gerar(Guid.NewGuid().ToString());
            var mockBlobStorageService = new Mock<IBlobStorageService>();
            var mockServiceBusClient = new Mock<ServiceBusClient>();
            var mockArchiveCreatedDispatcher = new Mock<ArchiveCreatedDispatcher>(mockServiceBusClient.Object);

            mockArchiveCreatedDispatcher.Setup(x => x.Dispatch(It.IsAny<ArchiveCreated>(), null)).Returns(Task.CompletedTask);

            var createArchive = new CreateArchiveUseCase(mockDatabase, mockBlobStorageService.Object, mockArchiveCreatedDispatcher.Object);

            var fixture = new Fixture();

            fixture.Register<byte[], Stream>((byte[] data) => new MemoryStream(data));

            var request = fixture.Create<ArchiveUploadRequest>();

            await createArchive.Execute(request);

            mockBlobStorageService.Verify(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>()), Times.Once);
            mockArchiveCreatedDispatcher.Verify(x => x.Dispatch(It.IsAny<ArchiveCreated>(), null), Times.Once);

            var archive = mockDatabase.Archives.First();
            Assert.NotNull(archive);
        }
    }
}
