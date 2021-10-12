using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImagineCupDiscord.Models;
using Microsoft.EntityFrameworkCore;

namespace ImagineCupDiscord.Integration.Infrastructure
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly ImagineCupDiscordDbContext _dbContext;

        public ParticipantRepository(ImagineCupDiscordDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Participant> FindAsync(string emailAddress) =>
            _dbContext.Participants.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);
    }
}
