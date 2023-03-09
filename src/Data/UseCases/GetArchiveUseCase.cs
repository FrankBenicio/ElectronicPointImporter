using Domain.Interfaces;
using Domain.Models;
using Domain.Requests;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.UseCases
{
    public class GetArchiveUseCase : IGetArchiveUseCase
    {
        private readonly DatabaseContext context;

        public GetArchiveUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Archive> Execute(ArchiveIdRequest archiveId)
        {
            return await context.Archives.FirstAsync(x => x.Id == archiveId.Id);
        }
    }
}
