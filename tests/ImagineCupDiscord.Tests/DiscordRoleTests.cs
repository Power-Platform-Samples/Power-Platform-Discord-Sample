using System;
using System.Threading.Tasks;
using Discord;
using ImagineCupDiscord.Integration.Exceptions;
using ImagineCupDiscord.Integration.Options;
using ImagineCupDiscord.Integration.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ImagineCupDiscord.Tests
{
    public class DiscordRoleTests
    {
        public DiscordRoleTests()
        {
            
        }

        [Fact]
        public async Task Discord_AssignRole()
        {
            // Arrange
            const ulong userId = 1234;
            var discordOptions = new DiscordOptions();

            var guildUserMock = new Mock<IGuildUser>();

            var guildMock = new Mock<IGuild>();
            guildMock.Setup(x => x.GetUserAsync(userId, It.IsAny<CacheMode>(), It.IsAny<RequestOptions>()))
                .ReturnsAsync(guildUserMock.Object);

            var discordClientMock = new Mock<IDiscordClient>();
            discordClientMock.Setup(x => x.GetGuildAsync(discordOptions.DiscordServerId, It.IsAny<CacheMode>(), It.IsAny<RequestOptions>()))
                .ReturnsAsync(guildMock.Object);

            var serverService = new ImagineCupDiscordServerService(discordClientMock.Object,
                new OptionsWrapper<DiscordOptions>(discordOptions));

            // Act
            await serverService.ApproveParticipantAsync(userId);

            // Assert: On success without exception thrown
        }

        [Fact]
        public async Task Discord_AssignRole_NotExistingUser()
        {
            // Arrange
            const ulong userId = 1234;
            var discordOptions = new DiscordOptions();


            var guildMock = new Mock<IGuild>();
            guildMock.Setup(x => x.GetUserAsync(userId, It.IsAny<CacheMode>(), It.IsAny<RequestOptions>()))
                .ReturnsAsync((IGuildUser) null);

            var discordClientMock = new Mock<IDiscordClient>();
            discordClientMock.Setup(x => x.GetGuildAsync(discordOptions.DiscordServerId, It.IsAny<CacheMode>(), It.IsAny<RequestOptions>()))
                .ReturnsAsync(guildMock.Object);

            var serverService = new ImagineCupDiscordServerService(discordClientMock.Object,
                new OptionsWrapper<DiscordOptions>(discordOptions));
            
            // Act & Assert
            await Assert.ThrowsAsync<ImagineCupParticipantMissingException>(() => 
                serverService.ApproveParticipantAsync(userId));
        }
    }
}