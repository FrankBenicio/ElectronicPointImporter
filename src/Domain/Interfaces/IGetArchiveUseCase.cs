using Domain.Models;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IGetArchiveUseCase
    {
        Task<Archive> Execute(ArchiveIdRequest id);
    }
}
