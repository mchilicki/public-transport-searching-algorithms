using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class RouteConfiguration : BaseEntityConfiguration<Route>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Route> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.ShortName)
                .IsRequired();
            builder.HasOne(p => p.Agency)
                .WithMany(p => p.Routes)
                .HasForeignKey("AgencyId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
