using Domain.BlobStorage;
using Domain.Interfaces;
using Domain.Models;
using Domain.Requests;
using Infra.Context;
using Infra.Events;
using System.IO;

namespace Data.UseCases
{
    public class CreateArchiveUseCase : ICreateArchiveUseCase
    {
        private readonly DatabaseContext context;
        private readonly IBlobStorageService blobStorageService;
        private readonly ArchiveCreatedDispatcher archiveCreated;

        public CreateArchiveUseCase(DatabaseContext context, IBlobStorageService blobStorageService, ArchiveCreatedDispatcher archiveCreated)
        {
            this.context = context;
            this.blobStorageService = blobStorageService;
            this.archiveCreated = archiveCreated;
        }

        public async Task Execute(ArchiveUploadRequest archiveUploadRequest)
        {
            var archive = new Archive(archiveUploadRequest.FileName);

            await blobStorageService.UploadAsync(archiveUploadRequest.File, archive.Directory);

            await context.AddAsync(archive);

            await context.SaveChangesAsync();

            await archiveCreated.Dispatch(new Domain.Events.ArchiveCreated(archive.Id));

        }
    }
}
