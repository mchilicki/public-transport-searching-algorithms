using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Data.Entities
{
    public class StopTime : BaseEntity
    {
        public Guid TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public Guid StopId { get; set; }
        public virtual Stop Stop { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int StopSequence { get; set; }

        public override string ToString()
        {
            return $"TripId: {TripId}, StopId: {StopId}, DepartureTime: {DepartureTime}, Sequence: {StopSequence}";
        }
    }

    public class StopTimeTripEqualityComparer : IEqualityComparer<StopTime>
    {
        public bool Equals(StopTime x, StopTime y)
        {
            return x.TripId == y.TripId;
        }

        public int GetHashCode(StopTime obj)
        {
            return obj.GetHashCode();
        }
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
