using System;
using System.Threading.Tasks;
using System.Web.Http;
using ImagineCupDiscord.Integration.Exceptions;
using ImagineCupDiscord.Integration.Services;
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
        private readonly VerificationService _verificationService;
        private readonly ImagineCupDiscordServerService _serverService;

        public ApproveParticipant(VerificationService verificationService, ImagineCupDiscordServerService serverService)
        {
            _verificationService = verificationService;
            _serverService = serverService;
        }

        [FunctionName(nameof(ApproveParticipant))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req)
        {
            // Get data
            var emailAddress = req.Form["emailAddress"];
            var discordUserId = req.Form["discordUserId"];

            // Validate
            if (string.IsNullOrEmpty(emailAddress))
                return new BadRequestErrorMessageResult("emailAddress is missing");

            if (!ulong.TryParse(discordUserId, out var userId))
                return new BadRequestErrorMessageResult("discordUserId is malformed");

                // Verify
            var isRegistered = await _verificationService.IsRegisteredAsync(emailAddress);
            if (!isRegistered)
                return new BadRequestErrorMessageResult("User is not registered for Imagine Cup");

            // TODO: Update user and add their user ID to database

            // Add roles
            try
            {
                await _serverService.ApproveParticipantAsync(userId);
            }
            catch (ImagineCupParticipantMissingException)
            {
                return new BadRequestErrorMessageResult("User is not part of the Imagine Cup Discord server");
            }

            return new AcceptedResult();
        }
    }
}