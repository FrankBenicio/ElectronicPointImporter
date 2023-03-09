using Domain.Interfaces;
using Domain.Models;
using Domain.Requests;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.UseCases
{
    public class GetListDepartmentPaymentByArchiveIdUseCase : IGetListDepartmentPaymentByArchiveIdUseCase
    {
        private readonly DatabaseContext context;

        public GetListDepartmentPaymentByArchiveIdUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<DepartmentPayment>> Execute(ArchiveIdRequest archiveId)
        {
            return await context.DepartmentsPayment.Include(x => x.Employees).Where(x => x.ArchiveId == archiveId.Id).ToListAsync();
        }
    }
}
