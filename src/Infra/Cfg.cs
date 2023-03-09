using Infra.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Azure.Messaging.ServiceBus;
using Infra.Events;
using Azure.Storage.Blobs;
using Infra.Storage;
using Domain.BlobStorage;
using Microsoft.AspNetCore.Builder;

namespace Infra
{
    [ExcludeFromCodeCoverage]
    public static class Cfg
    {
        public static void AddCfgDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DatabaseContext>(x =>
                   x.EnableSensitiveDataLogging()
                    .UseSqlServer(connectionString: Configuration.GetConnectionString("Database"), sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure())
               );
        }

        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            }
        }
        public static void AddEventDispatchers(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(new ServiceBusClient(Configuration.GetConnectionString("ServiceBus")));
            services.AddSingleton<ArchiveCreatedDispatcher>();
        }

        public static void AddBlobStorage(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(new BlobServiceClient(Configuration.GetConnectionString("BlobStorage")));
            services.AddScoped<IBlobStorageService, BlobStorageService>();

        }
    }
}
