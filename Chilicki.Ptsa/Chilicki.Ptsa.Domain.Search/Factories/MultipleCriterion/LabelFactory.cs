using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion
{
    public class LabelFactory
    {
        public ICollection<Label> CreateEmptyLabels()
        {
            return new List<Label>();
        }        

        public Label CreateLabel(Vertex vertex, Connection connection)
        {
            return new Label
            {
                Vertex = vertex,
                Connection = connection,
            };
        }

        public ICollection<Label> CreateStartLabels(
            Vertex vertex, SearchInput search)
        {
            var startTimePlusDelay = search.StartTime.Add(TimeSpan.FromHours(2));
            var possibleConnections = vertex.Connections
                .Where(p =>
                    p.DepartureTime >= search.StartTime &&
                    p.DepartureTime <= startTimePlusDelay
                );
            var labels = new List<Label>();
            foreach (var conn in possibleConnections)
            {
                var label = CreateLabel(vertex, conn);
                labels.Add(label);
            }
            return labels;
        }
    }
}
