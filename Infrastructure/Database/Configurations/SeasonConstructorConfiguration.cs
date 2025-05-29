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
    public class SeasonConstructorConfiguration : IEntityTypeConfiguration<SeasonConstructor>
    {
        public void Configure(EntityTypeBuilder<SeasonConstructor> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.HasOne(sc => sc.Season)
                   .WithMany(s => s.SeasonConstructors)
                   .HasForeignKey(sc => sc.SeasonId);

            builder.HasOne(sc => sc.Constructor)
                   .WithMany(c => c.SeasonConstructors)
                   .HasForeignKey(sc => sc.ConstructorId);

            builder.Property(sc => sc.StartingPrice)
                    .HasPrecision(10, 2);

            builder.Property(sc => sc.CurrentPrice)
                    .HasPrecision(10, 2);
        }
    }
}
