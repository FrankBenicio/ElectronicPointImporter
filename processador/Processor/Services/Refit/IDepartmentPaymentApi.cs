using Processor.Models;
using Refit;

namespace Processor.Services.Refit
{
    public interface IDepartmentPaymentApi
    {
        [Post("/department-payment")]
        Task Post(
            [Body] DepartmentPayment model);
    }
}
