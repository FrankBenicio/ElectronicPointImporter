using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IProcessArchiveUseCase
    {
        Task Execute(ArchiveIdRequest archiveId);
    }
}
