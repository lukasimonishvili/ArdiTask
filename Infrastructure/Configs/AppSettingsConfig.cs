using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configs
{
    public class AppSettingsConfig
    {
        public readonly string connectionString;

        public AppSettingsConfig()
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(Path.Combine("", "appsettings.json"))
                .Build();

            connectionString = configuration["ConnectionString"];
        }
    }
}
