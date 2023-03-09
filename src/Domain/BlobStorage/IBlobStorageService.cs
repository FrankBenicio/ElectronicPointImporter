namespace Domain.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(Stream file, string fileName);
    }
}
