using System;
using System.Threading.Tasks;
using Discord;
using ImagineCupDiscord.Integration.Exceptions;
using ImagineCupDiscord.Integration.Options;
using Microsoft.Extensions.Options;

namespace ImagineCupDiscord.Integration.Services
{
    public class ImagineCupDiscordServerService
    {
        private readonly IDiscordClient _discordClient;
        private readonly DiscordOptions _options;

        public ImagineCupDiscordServerService(IDiscordClient discordClient, IOptions<DiscordOptions> options)
        {
            _discordClient = discordClient;
            _options = options.Value;
        }

        /// <summary>
        /// Approves a participant based on their Discord user ID by applying the registered participant's role to the user.
        /// </summary>
        /// <param name="userId">The Discord user ID of the registered participant</param>
        public async Task ApproveParticipantAsync(ulong userId)
        {
            var guild = await _discordClient.GetGuildAsync(_options.DiscordServerId);
            var user = await guild.GetUserAsync(userId);
            if (user is null)
                throw new ImagineCupParticipantMissingException();

            await user.AddRoleAsync(_options.RegisteredParticipantRoleId);
        }
    }
}