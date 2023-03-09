using Domain.Models;
using Domain.Requests;

namespace Domain.Interfaces
{
    public interface IGetListDepartmentPaymentByArchiveIdUseCase
    {
        Task<List<DepartmentPayment>> Execute(ArchiveIdRequest archiveId);
    }
}
