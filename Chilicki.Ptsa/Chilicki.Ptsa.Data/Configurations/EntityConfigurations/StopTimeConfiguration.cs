using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class StopTimeConfiguration : BaseEntityConfiguration<StopTime>
    {
        public override void ConfigureEntity(EntityTypeBuilder<StopTime> builder)
        {
            builder.HasOne(p => p.Trip)
                .WithMany(p => p.StopTimes)
                .HasForeignKey("TripId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Stop)
                .WithMany()
                .HasForeignKey("StopId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(p => p.DepartureTime)
                .HasConversion(new TimeSpanToTicksConverter());
        }
    }
}
