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
    public class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired();

            builder.HasOne(r => r.Season)
                   .WithMany(s => s.Races)
                   .HasForeignKey(r => r.SeasonId);

            builder.HasMany(r => r.RaceResults)
                   .WithOne(rr => rr.Race)
                   .HasForeignKey(rr => rr.RaceId);

            builder.HasMany(r => r.ConstructorResults)
                   .WithOne(cr => cr.Race)
                   .HasForeignKey(cr => cr.RaceId);

            builder.HasMany(r => r.GameweekTeams)
                   .WithOne(gt => gt.Race)
                   .HasForeignKey(gt => gt.RaceId);
        }
    }
}
