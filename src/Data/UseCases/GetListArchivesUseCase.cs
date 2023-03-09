using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.UseCases
{
    public class GetListArchivesUseCase : IGetListArchivesUseCase
    {
        private readonly DatabaseContext context;

        public GetListArchivesUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Archive>> Execute()
        {
            return await context.Archives.ToListAsync();
        }
    }
}
