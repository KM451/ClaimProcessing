using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Infrastructure.ExternalAPI.INTAMI;
using ClaimProcessing.Infrastructure.FileStore;
using ClaimProcessing.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClaimProcessing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("IntamiClient", options =>
            {
                options.BaseAddress = new Uri("http://kodpocztowy.intami.pl");
                options.Timeout = new TimeSpan(0, 0, 10);
                options.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }).ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler());
            services.AddScoped<IIntamiClient, IntamiClient>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IFileStore, FileStore.FileStore>();
            services.AddTransient<IFileWrapper, FileWrapper>();
            services.AddTransient<IDirectoryWrapper, DirectoryWrapper>();   
            return services;
        }
    }
}