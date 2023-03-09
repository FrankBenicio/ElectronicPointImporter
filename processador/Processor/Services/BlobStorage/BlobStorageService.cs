using Azure.Storage.Blobs;
using System.IO;

namespace Processor.Services.BlobStorage
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobClient;
        private readonly string containerName;
        public BlobStorageService(BlobServiceClient blobClient, string containerName)
        {
            _blobClient = blobClient;
            this.containerName = containerName;
        }
        public async Task<byte[]> GetAsync(string fileName)
        {
            var container = await GetContainer(containerName).ConfigureAwait(false);

            var blockBlobReference = container.GetBlobClient(fileName);

            if (!await blockBlobReference.ExistsAsync().ConfigureAwait(false))
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                await blockBlobReference.DownloadToAsync(ms).ConfigureAwait(false);               
                return ms.ToArray();
            }
        }

        private async Task<BlobContainerClient> GetContainer(string containerName)
        {
            var container = _blobClient.GetBlobContainerClient(containerName);

            var exists =
                await container
                    .ExistsAsync()
                    .ConfigureAwait(false);

            if (!exists)
            {
                container =
                    await _blobClient
                        .CreateBlobContainerAsync(containerName)
                        .ConfigureAwait(false);
            }

            return container;
        }
    }


}
