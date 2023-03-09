using Azure.Storage.Blobs;
using Domain.BlobStorage;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Storage
{
    [ExcludeFromCodeCoverage]
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient blobClient;
        private readonly IConfiguration configuration;
        public BlobStorageService(BlobServiceClient blobClient, IConfiguration configuration)
        {
            this.blobClient = blobClient;
            this.configuration = configuration;
        }

        public async Task<string> UploadAsync(Stream file, string fileName)
        {
            var containerName = configuration["StorageConfiguration:ContainerName"];

            if (string.IsNullOrWhiteSpace(containerName))
                throw new NullReferenceException(message: "Container name not found");

            var container = await GetContainer(containerName).ConfigureAwait(false);

            var blockBlobReference = container.GetBlobClient(fileName);

            await blockBlobReference.UploadAsync(file, true).ConfigureAwait(false);

            return blockBlobReference.Uri.AbsoluteUri;
        }

        private async Task<BlobContainerClient> GetContainer(string containerName)
        {
            var container = blobClient.GetBlobContainerClient(containerName);

            var exists =
                await container
                    .ExistsAsync()
                    .ConfigureAwait(false);

            if (!exists)
            {
                container =
                    await blobClient
                        .CreateBlobContainerAsync(containerName)
                        .ConfigureAwait(false);
            }

            return container;
        }
    }
}
