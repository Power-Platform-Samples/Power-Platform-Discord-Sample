using ImagineCupDiscord.Integration;
using ImagineCupDiscord.Integration.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ImagineCupDiscord.Integration
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Configuration
            var context = builder.GetContext();
            var connectionString = context.Configuration.GetConnectionStringOrSetting("DefaultSqlConnection");

            // Database
            builder.Services.AddDbContext<ImagineCupDiscordDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}