using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class AgencyConfiguration : BaseEntityConfiguration<Agency>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Agency> builder)
        {
            builder.ToTable("Agencies");
            builder.Property(p => p.Name)
                .IsRequired();
        }
    }
}
