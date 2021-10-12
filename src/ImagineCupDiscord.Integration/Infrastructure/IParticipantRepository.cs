using System.Threading.Tasks;
using ImagineCupDiscord.Models;

namespace ImagineCupDiscord.Integration.Infrastructure
{
    public interface IParticipantRepository
    {
        Task<Participant> FindAsync(string emailAddress);
    }
}