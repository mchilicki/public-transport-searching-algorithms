using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Linq;

namespace Chilicki.Ptsa.Data.Entities
{
    public class StopTime : BaseEntity
    {
        public virtual Trip Trip { get; set; }
        public Guid StopId { get; set; }
        public virtual Stop Stop { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int StopSequence { get; set; }
    }

    public static class StopTimeExtensions
    {
        public static StopTime GetTripNextStopTime(this StopTime currentStopTime)
        {
            return currentStopTime.Trip
                .StopTimes
                .FirstOrDefault(p => 
                    p.StopSequence == currentStopTime.StopSequence + 1);
        }
    }
}
