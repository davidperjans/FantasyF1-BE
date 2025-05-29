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
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired();

            builder.HasOne(l => l.Owner)
                   .WithMany(u => u.OwnedLeagues)
                   .HasForeignKey(l => l.OwnerId);

            builder.HasOne(l => l.Season)
                   .WithMany()
                   .HasForeignKey(l => l.SeasonId);

            builder.HasMany(l => l.UserLeagues)
                   .WithOne(ul => ul.League)
                   .HasForeignKey(ul => ul.LeagueId);
        }
    }
}
