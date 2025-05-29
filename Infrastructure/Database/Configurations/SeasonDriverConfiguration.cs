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
    public class SeasonDriverConfiguration : IEntityTypeConfiguration<SeasonDriver>
    {
        public void Configure(EntityTypeBuilder<SeasonDriver> builder)
        {
            builder.HasKey(sd => sd.Id);

            builder.HasOne(sd => sd.Season)
                   .WithMany(s => s.SeasonDrivers)
                   .HasForeignKey(sd => sd.SeasonId);

            builder.HasOne(sd => sd.Driver)
                   .WithMany(d => d.SeasonDrivers)
                   .HasForeignKey(sd => sd.DriverId);

            builder.HasOne(sd => sd.Constructor)
                   .WithMany()
                   .HasForeignKey(sd => sd.ConstructorId);

            builder.Property(sd => sd.StartingPrice)
                    .HasPrecision(10, 2);

            builder.Property(sd => sd.CurrentPrice)
                    .HasPrecision(10, 2);
        }
    }
}
