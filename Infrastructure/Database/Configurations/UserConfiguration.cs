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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);

            builder.HasMany(u => u.FantasyTeams)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            builder.HasMany(u => u.UserLeagues)
                .WithOne(ul => ul.User)
                .HasForeignKey(ul => ul.UserId);

            builder.HasMany(u => u.OwnedLeagues)
                .WithOne(l => l.Owner)
                .HasForeignKey(l => l.OwnerId);
        }
    }

}
