using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SilkFlo.Web.Controllers2.FileUpload;

namespace SilkFlo.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBlobServices(this IServiceCollection services, IConfiguration configuration)
        {
            var blobStorageSettings = new BlobStorageSettings();
            configuration.GetSection(BlobStorageSettings.SettingName).Bind(blobStorageSettings);

            services.AddSingleton(x =>
            {
                return new BlobServiceClient(blobStorageSettings.ConnectionString);
            });

            // services.AddScoped<IFileStorageService, BlobStorageService>();
            services.AddScoped<IAzureStorage, AzureStorage>();

            return services;
        }
    }
}
