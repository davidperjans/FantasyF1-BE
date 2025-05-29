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
    public class GameweekDriverConfiguration : IEntityTypeConfiguration<GameweekDriver>
    {
        public void Configure(EntityTypeBuilder<GameweekDriver> builder)
        {
            builder.HasKey(gd => gd.Id);

            builder.HasOne(gd => gd.GameweekTeam)
                   .WithMany(gt => gt.GameweekDrivers)
                   .HasForeignKey(gd => gd.GameweekTeamId);

            builder.HasOne(gd => gd.Driver)
                   .WithMany()
                   .HasForeignKey(gd => gd.DriverId);

            builder.Property(gd => gd.Price)
                    .HasPrecision(10, 2);
        }
    }
}
