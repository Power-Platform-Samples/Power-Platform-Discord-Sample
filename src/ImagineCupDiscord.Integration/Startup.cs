using Discord;
using Discord.Rest;
using ImagineCupDiscord.Integration;
using ImagineCupDiscord.Integration.Infrastructure;
using ImagineCupDiscord.Integration.Options;
using ImagineCupDiscord.Integration.Services;
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

            builder.Services.Configure<DiscordOptions>(context.Configuration.GetSection("Discord"));

            // Infrastructure
            builder.Services.AddDbContext<ImagineCupDiscordDbContext>(x => x.UseSqlServer(connectionString));
            builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();

            // Discord
            builder.Services.AddOptions<DiscordOptions>()
                .Configure(options => builder.Services.AddSingleton<IDiscordClient, DiscordRestClient>(_ =>
                {
                    var client = new DiscordRestClient();
                    client.LoginAsync(TokenType.Bot, options.BotToken).Wait();

                    return client;
                }));

            // Domain Services
            builder.Services.AddScoped<VerificationService>();
            builder.Services.AddSingleton<ImagineCupDiscordServerService>();
        }
    }
}