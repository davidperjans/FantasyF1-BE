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
    public class TeamConstructorConfiguration : IEntityTypeConfiguration<TeamConstructor>
    {
        public void Configure(EntityTypeBuilder<TeamConstructor> builder)
        {
            builder.HasKey(tc => tc.Id);

            builder.HasOne(tc => tc.FantasyTeam)
                   .WithMany(ft => ft.TeamConstructors)
                   .HasForeignKey(tc => tc.FantasyTeamId);

            builder.HasOne(tc => tc.Constructor)
                   .WithMany(c => c.TeamConstructors)
                   .HasForeignKey(tc => tc.ConstructorId);

            builder.Property(tc => tc.PurchasePrice)
                    .HasPrecision(10, 2);
        }
    }
}
