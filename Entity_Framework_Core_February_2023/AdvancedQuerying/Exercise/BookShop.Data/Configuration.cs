using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BookShop.Data;

internal class Configuration
{
    private static IConfiguration userSecrets = new ConfigurationBuilder()
        .AddUserSecrets<BookShopContext>()
        .Build();

    internal static string ConnectionString
        => userSecrets["ConnectionStrings:BookShop"];
}
