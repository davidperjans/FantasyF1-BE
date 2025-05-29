using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.RelationshipEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configurations
{
    public class TeamDriverConfiguration : IEntityTypeConfiguration<TeamDriver>
    {
        public void Configure(EntityTypeBuilder<TeamDriver> builder)
        {
            builder.HasKey(td => td.Id);

            builder.HasOne(td => td.FantasyTeam)
                   .WithMany(ft => ft.TeamDrivers)
                   .HasForeignKey(td => td.FantasyTeamId);

            builder.HasOne(td => td.Driver)
                   .WithMany(d => d.TeamDrivers)
                   .HasForeignKey(td => td.DriverId);

            builder.Property(td => td.PurchasePrice)
                    .HasPrecision(10, 2);
        }
    }
}
