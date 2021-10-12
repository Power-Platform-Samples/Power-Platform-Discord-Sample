using System;

namespace ImagineCupDiscord.Models
{
    public class Registration : Entity
    {
        public Participant Participant { get; set; }

        /// <summary>
        /// The participant's Discord User ID.
        /// </summary>
        public ulong DiscordUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime JoinDate { get; set; }

        public bool IsVerified { get; set; }

        public DateTime VerificationDate { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
