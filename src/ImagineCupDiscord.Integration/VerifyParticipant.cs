using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using ImagineCupDiscord.Integration.Infrastructure;
using ImagineCupDiscord.Integration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ImagineCupDiscord.Integration
{
    public class VerifyParticipant
    {
        private readonly VerificationService _verificationService;

        public VerifyParticipant(VerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        [FunctionName(nameof(VerifyParticipant))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            if (!req.HasFormContentType)
                return new BadRequestErrorMessageResult("Request must be form content type");

            // Get email address
            var emailAddress = req.Form["emailAddress"];
            if (string.IsNullOrEmpty(emailAddress))
                return new BadRequestErrorMessageResult("emailAddress not given in form");

            // Verify
            var isRegistered = await _verificationService.IsRegisteredAsync(emailAddress);

            // Return
            return isRegistered ? (IActionResult) new OkResult() : new NotFoundResult();
        }
    }
}
