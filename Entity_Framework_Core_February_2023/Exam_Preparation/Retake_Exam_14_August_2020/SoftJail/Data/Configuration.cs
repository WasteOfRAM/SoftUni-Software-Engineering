using Microsoft.Extensions.Configuration;

namespace SoftJail.Data
{
    public static class Configuration
    {
        private static IConfiguration userSecrets = new ConfigurationBuilder()
            .AddUserSecrets<StartUp>()
            .Build();

        public static string ConnectionString
            => userSecrets["ConnectionStrings:SoftJail"];
    }
}
