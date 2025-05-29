using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations
{
    public class FantasyTeamConfiguration : IEntityTypeConfiguration<FantasyTeam>
    {
        public void Configure(EntityTypeBuilder<FantasyTeam> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.TeamName).IsRequired().HasMaxLength(100);

            builder.HasOne(f => f.User)
                   .WithMany(u => u.FantasyTeams)
                   .HasForeignKey(f => f.UserId);

            builder.HasOne(f => f.Season)
                   .WithMany(s => s.FantasyTeams)
                   .HasForeignKey(f => f.SeasonId);

            builder.HasMany(f => f.TeamDrivers)
                   .WithOne(td => td.FantasyTeam)
                   .HasForeignKey(td => td.FantasyTeamId);

            builder.HasMany(f => f.TeamConstructors)
                   .WithOne(tc => tc.FantasyTeam)
                   .HasForeignKey(tc => tc.FantasyTeamId);

            builder.HasMany(f => f.GameweekTeams)
                   .WithOne(gw => gw.FantasyTeam)
                   .HasForeignKey(gw => gw.FantasyTeamId);

            builder.Property(f => f.Budget)
                    .HasPrecision(10, 2);
        }
    }
}
