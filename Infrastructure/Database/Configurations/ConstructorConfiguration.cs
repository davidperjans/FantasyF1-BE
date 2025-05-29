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
    public class ConstructorConfiguration : IEntityTypeConfiguration<Constructor>
    {
        public void Configure(EntityTypeBuilder<Constructor> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();

            builder.HasMany(c => c.SeasonConstructors)
                   .WithOne(sc => sc.Constructor)
                   .HasForeignKey(sc => sc.ConstructorId);

            builder.HasMany(c => c.TeamConstructors)
                   .WithOne(tc => tc.Constructor)
                   .HasForeignKey(tc => tc.ConstructorId);

            builder.HasMany(c => c.ConstructorResults)
                   .WithOne(cr => cr.Constructor)
                   .HasForeignKey(cr => cr.ConstructorId);

            builder.Property(c => c.CurrentPrice)
                    .HasPrecision(10, 2);
        }
    }
}
