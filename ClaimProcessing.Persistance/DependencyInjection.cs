using ClaimProcessing.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClaimProcessing.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ClaimProcessingDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ClaimDatabase")));
            services.AddScoped<IClaimProcessingDbContext, ClaimProcessingDbContext>();
            return services;
        }

        public static IServiceCollection AddPersistanceInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClaimProcessingDbContext>(options => options.UseInMemoryDatabase("InMemoryDatabase"));
            //services.AddScoped<IClaimProcessingDbContext, ClaimProcessingDbContext>();
            return services;
        }
    }
}
