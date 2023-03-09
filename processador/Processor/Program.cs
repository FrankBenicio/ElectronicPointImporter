using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Processor.Services.BlobStorage;
using Processor.Services.Refit;
using Refit;

var urlUrl = Environment.GetEnvironmentVariable("urlUrl") ?? string.Empty;
var blobStorage = Environment.GetEnvironmentVariable("blobStorage") ?? string.Empty;
var containerName = Environment.GetEnvironmentVariable("containerName") ?? string.Empty;


var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(x =>
    {
        x.AddRefitClient<IArchiveApi>()
        .ConfigureHttpClient(client => client.BaseAddress = new Uri(urlUrl));
        x.AddRefitClient<IDepartmentPaymentApi>()
        .ConfigureHttpClient(client => client.BaseAddress = new Uri(urlUrl));
        x.AddSingleton(new BlobStorageService(new BlobServiceClient(blobStorage), containerName));
    })
    .Build();

host.Run();
