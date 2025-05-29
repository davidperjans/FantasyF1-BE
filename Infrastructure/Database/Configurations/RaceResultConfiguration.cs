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
    public class RaceResultConfiguration : IEntityTypeConfiguration<RaceResult>
    {
        public void Configure(EntityTypeBuilder<RaceResult> builder)
        {
            builder.HasKey(rr => rr.Id);

            builder.HasOne(rr => rr.Race)
                   .WithMany(r => r.RaceResults)
                   .HasForeignKey(rr => rr.RaceId);

            builder.HasOne(rr => rr.Driver)
                   .WithMany(d => d.RaceResults)
                   .HasForeignKey(rr => rr.DriverId);
        }
    }
}
