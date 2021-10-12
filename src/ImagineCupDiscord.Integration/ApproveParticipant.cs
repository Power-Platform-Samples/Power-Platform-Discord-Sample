using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace ImagineCupDiscord.Integration
{
    /// <summary>
    /// This Function verifies the participant and adds all necessary roles for the Imagine Cup.
    /// </summary>
    public class ApproveParticipant
    {
        public ApproveParticipant()
        {
            
        }

        [FunctionName(nameof(ApproveParticipant))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req)
        {
            return new OkResult();
        }
    }
}