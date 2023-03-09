using Domain.Interfaces;
using Domain.Models;
using Infra.Context;

namespace Data.UseCases
{
    public class CreateDepartmentPaymentUseCase : ICreateDepartmentPaymentUseCase
    {
        private readonly DatabaseContext context;

        public CreateDepartmentPaymentUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(DepartmentPayment model)
        {
            await context.AddAsync(model);
            await context.SaveChangesAsync();
        }
    }
}
