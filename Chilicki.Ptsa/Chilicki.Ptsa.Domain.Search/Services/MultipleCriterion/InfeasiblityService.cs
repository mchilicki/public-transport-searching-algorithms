using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion
{
    public class InfeasiblityService
    {
        public bool IsInfeasible(Connection conn, IEnumerable<Label> currentLabels)
        {
            if (IsConnectionSameAsOneOf(conn, currentLabels))
                return true;
            return false;
        }

        private bool IsConnectionSameAsOneOf(Connection conn, IEnumerable<Label> currentLabels)
        {
            return currentLabels.Select(p => p.Connection)
                .Any(currentConn => IsConnectionSameAsOneOf(conn, currentConn));           
        }

        private bool IsConnectionSameAsOneOf(Connection conn, Connection currentConn)
        {
            return conn.Id == currentConn.Id;
        }
    }
}
