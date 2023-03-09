using Domain.Interfaces;
using Domain.Requests;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.UseCases
{
    public class ProcessArchiveUseCase : IProcessArchiveUseCase
    {
        private readonly DatabaseContext context;

        public ProcessArchiveUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(ArchiveIdRequest archiveId)
        {
            var archive = await context.Archives.FirstAsync(x => x.Id == archiveId.Id);

            archive.Process();

            // Stryker disable all
            await context.SaveChangesAsync();
        }
    }
}
