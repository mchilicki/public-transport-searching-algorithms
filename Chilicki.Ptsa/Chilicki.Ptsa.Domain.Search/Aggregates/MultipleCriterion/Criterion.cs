using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion
{
    public class Criterion
    {        
        public int Value { get; set; }

        public static Criterion CreateZero
        {
            get
            {
                return Create(0);
            }
        }

        public static Criterion CreateOne
        {
            get
            {
                return Create(1);
            }
        }

        public static Criterion Create(int value)
        {
            return new Criterion
            {
                Value = value,
            };
        }

        public static Criterion CreateOneMore(Criterion criterion)
        {
            return Create(criterion.Value + 1);
        }

        public static Criterion CreateEqual(Criterion criterion)
        {
            return Create(criterion.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
