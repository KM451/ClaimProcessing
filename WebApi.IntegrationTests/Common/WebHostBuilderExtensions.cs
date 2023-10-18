using System.Reflection;

namespace WebApi.IntegrationTests.Common
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseConfiguration(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    config.AddUserSecrets(appAssembly, optional: true);
                }
            });

            return builder;
        }
    }
}
