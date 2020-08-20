using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Helpers.Extensions
{
    public static class RandomExtensions
    {
        public static TimeSpan NextTimeSpan(this Random random)
        {
            var startTimeSpan = TimeSpan.FromHours(10);
            var endTimeSpan = TimeSpan.FromHours(17);
            var randomMinutes = random.Next((int)startTimeSpan.TotalMinutes, (int)endTimeSpan.TotalMinutes);
            return TimeSpan.FromMinutes(randomMinutes);
        }
    }
}
