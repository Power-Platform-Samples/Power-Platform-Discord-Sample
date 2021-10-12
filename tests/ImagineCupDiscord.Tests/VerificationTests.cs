using System;
using System.Threading.Tasks;
using ImagineCupDiscord.Integration;
using ImagineCupDiscord.Integration.Infrastructure;
using ImagineCupDiscord.Integration.Services;
using ImagineCupDiscord.Models;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ImagineCupDiscord.Tests
{
    public class VerificationTests
    {
        [Fact]
        public async Task Verify_ExistingEmailAddress()
        {
            // Arrange
            const string emailAddress = "martha@example.com";
            var repositoryMock = new Mock<IParticipantRepository>();
            repositoryMock.Setup(x => x.FindAsync(emailAddress)).ReturnsAsync(new Participant
            {
                EmailAddress = emailAddress,
                Id = "1234"
            });

            var verificationService = new VerificationService(repositoryMock.Object);

            // Act
            var result = await verificationService.IsRegisteredAsync(emailAddress);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Verify_EmailAddressNotFound()
        {
            // Arrange
            const string emailAddress = "martha@example.com";
            var repositoryMock = new Mock<IParticipantRepository>();
            repositoryMock.Setup(x => x.FindAsync(emailAddress)).ReturnsAsync((Participant) null);

            var verificationService = new VerificationService(repositoryMock.Object);

            // Act
            var result = await verificationService.IsRegisteredAsync(emailAddress);

            // Assert
            Assert.False(result);
        }
    }
}
