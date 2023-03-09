
using Processor.Models;
using Refit;

namespace Processor.Services.Refit
{
    public interface IArchiveApi
    {
        [Get("/archives/{id}")]
        Task<Archive> GetArchive(
            [Query] Guid id);

        [Put("/archives/process/{id}")]
        Task ProcessArchive(
            [Query] Guid id);

        [Put("/archives/finalize/{id}")]
        Task FinalizeArchive(
            [Query] Guid id);
    }
}
