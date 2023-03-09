using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IFinalizeArchiveUseCase
    {
        Task Execute(ArchiveIdRequest archiveId);
    }
}
