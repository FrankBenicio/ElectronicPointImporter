using Domain.Interfaces;
using Domain.Requests;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.UseCases
{
    public class FinalizeArchiveUseCase : IFinalizeArchiveUseCase
    {
        private readonly DatabaseContext context;

        public FinalizeArchiveUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(ArchiveIdRequest archiveId)
        {
            var archive = await context.Archives.FirstAsync(x => x.Id == archiveId.Id);

            archive.Finalize();

            // Stryker disable all
            await context.SaveChangesAsync();
        }
    }
}
