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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.FirstName).IsRequired();
            builder.Property(d => d.LastName).IsRequired();
            builder.Property(d => d.Code).IsRequired().HasMaxLength(5);

            builder.HasMany(d => d.SeasonDrivers)
                   .WithOne(sd => sd.Driver)
                   .HasForeignKey(sd => sd.DriverId);

            builder.HasMany(d => d.TeamDrivers)
                   .WithOne(td => td.Driver)
                   .HasForeignKey(td => td.DriverId);

            builder.HasMany(d => d.RaceResults)
                   .WithOne(rr => rr.Driver)
                   .HasForeignKey(rr => rr.DriverId);

            builder.Property(d => d.CurrentPrice)
                    .HasPrecision(10, 2);
        }
    }
}
