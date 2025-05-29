using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ResultEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations
{
    public class GameweekTeamConfiguration : IEntityTypeConfiguration<GameweekTeam>
    {
        public void Configure(EntityTypeBuilder<GameweekTeam> builder)
        {
            builder.HasKey(gt => gt.Id);

            builder.HasOne(gt => gt.FantasyTeam)
                   .WithMany(ft => ft.GameweekTeams)
                   .HasForeignKey(gt => gt.FantasyTeamId);

            builder.HasOne(gt => gt.Race)
                   .WithMany(r => r.GameweekTeams)
                   .HasForeignKey(gt => gt.RaceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
