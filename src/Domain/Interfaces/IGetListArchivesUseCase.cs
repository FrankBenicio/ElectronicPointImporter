using Domain.Models;

namespace Domain.Interfaces
{
    public interface IGetListArchivesUseCase
    {
        Task<List<Archive>> Execute();
    }
}
