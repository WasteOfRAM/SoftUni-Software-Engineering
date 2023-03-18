using Microsoft.Extensions.Configuration;

namespace ProductShop.Data
{
    public static class Configuration
    {
        private static IConfiguration userSecrets = new ConfigurationBuilder()
            .AddUserSecrets<StartUp>()
            .Build();

        public static readonly string ConnectionString = userSecrets["ConnectionStrings:ProductsShop"];
    }
}
