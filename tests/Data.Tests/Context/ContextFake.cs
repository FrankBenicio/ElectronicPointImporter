using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.Context
{
    public static class ContextFake
    {
        public static async Task<DatabaseContext> Gerar(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var context = new DatabaseContext(options);

            return await Task.FromResult(context);
        }
    }
}
