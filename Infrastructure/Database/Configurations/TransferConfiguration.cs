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
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TransferDate)
                .IsRequired();

            // Relation till FantasyTeam
            builder.HasOne(t => t.FantasyTeam)
                .WithMany(ft => ft.Transfers)
                .HasForeignKey(t => t.FantasyTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Du kan välja att konfigurera relationer till Driver och Constructor om du vill

            builder.Property(t => t.OutDriverId)
                .IsRequired(false);

            builder.Property(t => t.InDriverId)
                .IsRequired(false);

            builder.Property(t => t.OutConstructorId)
                .IsRequired(false);

            builder.Property(t => t.InConstructorId)
                .IsRequired(false);
        }
    }
}
