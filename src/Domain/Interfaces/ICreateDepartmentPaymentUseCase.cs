using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICreateDepartmentPaymentUseCase
    {
        Task Execute(DepartmentPayment model);
    }
}
