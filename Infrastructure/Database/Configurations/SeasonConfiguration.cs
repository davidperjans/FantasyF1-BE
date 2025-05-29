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
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired();

            builder.HasMany(s => s.Races)
                   .WithOne(r => r.Season)
                   .HasForeignKey(r => r.SeasonId);

            builder.HasMany(s => s.SeasonDrivers)
                   .WithOne(sd => sd.Season)
                   .HasForeignKey(sd => sd.SeasonId);

            builder.HasMany(s => s.SeasonConstructors)
                   .WithOne(sc => sc.Season)
                   .HasForeignKey(sc => sc.SeasonId);

            builder.HasMany(s => s.FantasyTeams)
                   .WithOne(ft => ft.Season)
                   .HasForeignKey(ft => ft.SeasonId);
        }
    }
}
