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

        public async Task<bool> IsRegisteredAsync(string emailAddress)
        {
            var participant = await _participantRepository.FindAsync(emailAddress);
            return participant != null;
        }
    }
}
