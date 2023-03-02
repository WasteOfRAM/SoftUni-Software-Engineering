using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MusicHub.Data
{
    public static class Configuration
    {
        public static IConfiguration ConnectionString = new ConfigurationBuilder()
            .AddUserSecrets<StartUp>()
            .Build();
    }
}
