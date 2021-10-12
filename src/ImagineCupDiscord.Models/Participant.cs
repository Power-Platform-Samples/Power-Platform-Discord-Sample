namespace ImagineCupDiscord.Models
{
    public class Participant : Entity
    {
        /// <summary>
        /// The participant's Discord User ID.
        /// </summary>
        public ulong DiscordUserId { get; set; }
    }
}
