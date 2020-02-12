using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class StopConfiguration : BaseEntityConfiguration<Stop>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Stop> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired();
        }
    }
}
