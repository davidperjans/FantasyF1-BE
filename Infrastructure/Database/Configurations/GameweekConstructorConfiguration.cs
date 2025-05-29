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
    public class GameweekConstructorConfiguration : IEntityTypeConfiguration<GameweekConstructor>
    {
        public void Configure(EntityTypeBuilder<GameweekConstructor> builder)
        {
            builder.HasKey(gc => gc.Id);

            builder.HasOne(gc => gc.GameweekTeam)
                   .WithMany(gt => gt.GameweekConstructors)
                   .HasForeignKey(gc => gc.GameweekTeamId);

            builder.HasOne(gc => gc.Constructor)
                   .WithMany()
                   .HasForeignKey(gc => gc.ConstructorId);

            builder.Property(gc => gc.Price)
                    .HasPrecision(10, 2);
        }
    }
}
