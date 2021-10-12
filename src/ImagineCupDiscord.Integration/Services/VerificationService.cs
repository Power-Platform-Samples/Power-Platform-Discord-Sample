using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImagineCupDiscord.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ImagineCupDiscord.Integration.Services
{
    public class VerificationService
    {
        private readonly IParticipantRepository _participantRepository;

        public VerificationService(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        /// <summary>
        /// Checks whether the user is registered for Imagine Cup based on their provided <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address used to register for IC.</param>
        /// <returns>Whether the user is registered for IC.</returns>
        public async Task<bool> IsRegisteredAsync(string emailAddress)
        {
            var participant = await _participantRepository.FindAsync(emailAddress);
            return participant != null;
        }
    }
}
