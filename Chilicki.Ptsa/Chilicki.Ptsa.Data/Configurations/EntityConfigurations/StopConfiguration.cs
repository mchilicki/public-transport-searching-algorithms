﻿using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class StopConfiguration : BaseEntityConfiguration<Stop>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Stop> builder)
        {
            builder.ToTable("Stops");
            builder.Property(p => p.Name)
                .IsRequired();
        }
    }
}
