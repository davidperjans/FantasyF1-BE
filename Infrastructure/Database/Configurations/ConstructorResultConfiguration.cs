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
    public class ConstructorResultConfiguration : IEntityTypeConfiguration<ConstructorResult>
    {
        public void Configure(EntityTypeBuilder<ConstructorResult> builder)
        {
            builder.HasKey(cr => cr.Id);

            builder.HasOne(cr => cr.Race)
                   .WithMany(r => r.ConstructorResults)
                   .HasForeignKey(cr => cr.RaceId);

            builder.HasOne(cr => cr.Constructor)
                   .WithMany(c => c.ConstructorResults)
                   .HasForeignKey(cr => cr.ConstructorId);
        }
    }
}
