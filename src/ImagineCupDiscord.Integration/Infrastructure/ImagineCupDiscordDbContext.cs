using System;
using System.Collections.Generic;
using System.Text;
using ImagineCupDiscord.Models;
using Microsoft.EntityFrameworkCore;

namespace ImagineCupDiscord.Integration.Infrastructure
{
    public class ImagineCupDiscordDbContext : DbContext
    {
        public DbSet<Participant> Participants { get; set; }

        public DbSet<Registration> Registrations { get; set; }
        
        /// <inheritdoc />
        protected ImagineCupDiscordDbContext()
        {
            Database.EnsureCreated();
        }

        /// <inheritdoc />
        public ImagineCupDiscordDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
