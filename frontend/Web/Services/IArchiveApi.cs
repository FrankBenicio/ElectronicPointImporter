using Microsoft.AspNetCore.Mvc;
using Refit;
using Web.Models;

namespace Web.Services
{
    public interface IArchiveApi
    {
        [Get("/archives")]
        Task<List<Archive>> GetArchives();

        [Multipart]
        [Post("/archives/upload")]
        Task PostArchives([AliasAs("archives")] IEnumerable<StreamPart> streams);
    }
}
