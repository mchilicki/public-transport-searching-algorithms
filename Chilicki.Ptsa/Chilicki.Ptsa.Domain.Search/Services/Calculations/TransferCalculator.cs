using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Calculations
{
    public class TransferCalculator
    {
        public Criterion CalculateTransferCriterion(Label currentLabel, Connection connection)
        {
            bool shouldBeTransfer = currentLabel.Connection.TripId != connection.TripId;
            if (shouldBeTransfer)
                return Criterion.CreateOneMore(currentLabel.TransferCriterion);
            else
                return Criterion.CreateEqual(currentLabel.TransferCriterion);
        }
    }
}
