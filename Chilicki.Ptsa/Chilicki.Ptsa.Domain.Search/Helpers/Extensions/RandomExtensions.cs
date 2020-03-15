﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Helpers.Extensions
{
    public static class RandomExtensions
    {
        public static TimeSpan NextTimeSpan(this Random random)
        {
            var startTimeSpan = TimeSpan.FromMinutes(1);
            var endTimeSpan = TimeSpan.FromDays(1).Subtract(startTimeSpan);
            var randomMinutes = random.Next((int)startTimeSpan.TotalMinutes, (int)endTimeSpan.TotalMinutes);
            return TimeSpan.FromMinutes(randomMinutes);
        }
    }
}
