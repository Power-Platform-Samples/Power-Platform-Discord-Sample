using System.Threading.Tasks;
using ImagineCupDiscord.Models;

namespace ImagineCupDiscord.Integration.Infrastructure
{
    public interface IParticipantRepository
    {
        /// <summary>
        /// Finds a <see cref="Participant"/> based on their <paramref name="emailAddress"/>.
        /// </summary>
        Task<Participant> FindAsync(string emailAddress);
    }
}