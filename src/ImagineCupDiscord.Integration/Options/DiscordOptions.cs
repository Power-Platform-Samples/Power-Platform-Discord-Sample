using System;
using System.Collections.Generic;
using System.Text;

namespace ImagineCupDiscord.Integration.Options
{
    public class DiscordOptions
    {
        public string BotToken { get; set; }

        public ulong DiscordServerId { get; set; }

        public ulong RegisteredParticipantRoleId { get; set; }
    }
}
