using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class ConnectionConfiguration : BaseEntityConfiguration<Connection>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Connection> builder)
        {
            builder.HasOne(p => p.Trip)
                .WithMany()
                .HasForeignKey("TripId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.StartVertex)
                .WithMany(p => p.Connections)
                .HasForeignKey("StartVertexId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.EndVertex)
                .WithMany()
                .HasForeignKey("EndVertexId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.StartStopTime)
                .WithMany()
                .HasForeignKey("StartStopTimeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.EndStopTime)
                .WithMany()
                .HasForeignKey("EndStopTimeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
