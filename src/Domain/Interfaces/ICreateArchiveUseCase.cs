using Domain.Models;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface ICreateArchiveUseCase
    {
        Task Execute(ArchiveUploadRequest archiveUploadRequest);
    }
}
