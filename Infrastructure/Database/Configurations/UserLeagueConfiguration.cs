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
    public class UserLeagueConfiguration : IEntityTypeConfiguration<UserLeague>
    {
        public void Configure(EntityTypeBuilder<UserLeague> builder)
        {
            builder.HasKey(ul => ul.Id);

            builder.HasOne(ul => ul.User)
                   .WithMany(u => u.UserLeagues)
                   .HasForeignKey(ul => ul.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ul => ul.League)
                   .WithMany(l => l.UserLeagues)
                   .HasForeignKey(ul => ul.LeagueId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
