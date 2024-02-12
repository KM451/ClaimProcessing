using ClaimProcessing.Client.HttpRepository.Interfaces;

namespace ClaimProcessing.Client.HttpRepository
{
    public static class HttpRepositories
    {
        public static IServiceCollection AddHttpRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClaimsHttpRepository, ClaimsHttpRepository>();
            services.AddScoped<ISupplierHttpRepository, SupplierHttpRepository>();
            services.AddScoped<IFotoHttpRepository, FotoHttpRepository>();
            return services;
        }
    }
}
