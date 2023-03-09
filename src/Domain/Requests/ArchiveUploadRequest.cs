namespace Domain.Requests
{
    public class ArchiveUploadRequest
    {
        public string FileName { get; set; }
        public Stream File { get; set; }
    }
}
