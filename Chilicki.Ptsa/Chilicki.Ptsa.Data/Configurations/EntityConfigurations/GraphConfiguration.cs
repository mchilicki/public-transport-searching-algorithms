using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class GraphConfiguration : BaseEntityConfiguration<Graph>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Graph> builder)
        {
            builder.ToTable("Graphs");
        }
    }
}
