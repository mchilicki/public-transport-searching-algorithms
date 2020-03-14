using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class ConnectionConfiguration : BaseEntityConfiguration<Connection>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Connection> builder)
        {
            builder.ToTable("Connections");
            builder.Property(p => p.DepartureTime)
                .HasConversion(new TimeSpanToTicksConverter());
            builder.Property(p => p.ArrivalTime)
                .HasConversion(new TimeSpanToTicksConverter());
            builder.HasOne(p => p.Graph)
                .WithMany(p => p.Connections)
                .HasForeignKey("GraphId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Trip>()
                .WithMany()
                .HasForeignKey(p => p.TripId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.StartVertex)
                .WithMany(p => p.Connections)
                .HasForeignKey(p => p.StartVertexId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.EndVertex)
                .WithMany()
                .HasForeignKey(p => p.EndVertexId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
