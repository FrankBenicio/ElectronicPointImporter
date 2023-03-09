using Microsoft.AspNetCore.Http;

namespace Web.Models
{
    public class ArchiveCommand
    {
        public List<IFormFile> Archives { get; set; }
    }
}
