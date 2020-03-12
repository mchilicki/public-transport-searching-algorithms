using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class VertexConfiguration : BaseEntityConfiguration<Vertex>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Vertex> builder)
        {
            builder.ToTable("Vertices");
            builder.Ignore(p => p.IsVisited);
            builder.HasOne(p => p.Graph)
                .WithMany(p => p.Vertices)
                .HasForeignKey("GraphId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Stop)
                .WithMany()
                .HasForeignKey(p => p.StopId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.SimilarVertices)
                .WithOne()
                .HasForeignKey("SimilarVertexId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
