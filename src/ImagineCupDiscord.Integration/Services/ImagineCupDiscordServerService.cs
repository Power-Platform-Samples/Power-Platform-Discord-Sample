﻿using System.Threading.Tasks;
using Discord;
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

        public async Task ApproveParticipantAsync(ulong userId)
        {
            var guild = await _discordClient.GetGuildAsync(_options.DiscordServerId);
            var user = await guild.GetUserAsync(userId);
            await user.AddRoleAsync(_options.RegisteredParticipantRoleId);
        }
    }
}