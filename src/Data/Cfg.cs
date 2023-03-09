using Data.UseCases;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Data
{
    [ExcludeFromCodeCoverage]
    public static class Cfg
    {
        public static void AddCfgData(this IServiceCollection services)
        {
            services.AddScoped<ICreateArchiveUseCase, CreateArchiveUseCase>();
            services.AddScoped<IProcessArchiveUseCase, ProcessArchiveUseCase>();
            services.AddScoped<IFinalizeArchiveUseCase, FinalizeArchiveUseCase>();
            services.AddScoped<ICreateDepartmentPaymentUseCase, CreateDepartmentPaymentUseCase>();
            services.AddScoped<IGetArchiveUseCase, GetArchiveUseCase>();
            services.AddScoped<IGetListArchivesUseCase, GetListArchivesUseCase>();
            services.AddScoped<IGetListDepartmentPaymentByArchiveIdUseCase, GetListDepartmentPaymentByArchiveIdUseCase>();
        }
    }
}
