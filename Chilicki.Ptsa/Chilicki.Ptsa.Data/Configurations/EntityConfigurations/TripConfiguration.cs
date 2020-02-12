using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class TripConfiguration : BaseEntityConfiguration<Trip>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Trip> builder)
        {
            builder.Property(p => p.HeadSign)
                .IsRequired();
            builder.HasOne(p => p.Route)
                .WithMany(p => p.Trips)
                .HasForeignKey("RouteId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
