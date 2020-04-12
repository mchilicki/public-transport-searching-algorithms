using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion
{
    public class Criterion
    {
        public int Value { get; set; }

        public static Criterion Create(int value)
        {
            return new Criterion
            {
                Value = value,
            };
        }
    }
}
