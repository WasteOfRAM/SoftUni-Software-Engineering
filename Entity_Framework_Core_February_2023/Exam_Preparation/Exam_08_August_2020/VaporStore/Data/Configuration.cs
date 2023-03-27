using Microsoft.Extensions.Configuration;

namespace VaporStore.Data
{
    public static class Configuration
    {
        private static IConfiguration userSecrets = new ConfigurationBuilder()
            .AddUserSecrets<StartUp>()
            .Build();

        public static string ConnectionString
            => userSecrets["ConnectionStrings:VaporStore"];
    }
}