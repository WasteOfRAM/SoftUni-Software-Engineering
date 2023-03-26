using Microsoft.Extensions.Configuration;

namespace Trucks.Data
{
    public static class Configuration
    {
        private static IConfiguration userSecrets = new ConfigurationBuilder()
        .AddUserSecrets<StartUp>()
        .Build();

        public static string ConnectionString
            => userSecrets["ConnectionStrings:Trucks"];
    }
}