using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion
{
    public class LabelFactory
    {
        readonly PossibleConnectionsService possibleConnectionsService;

        public LabelFactory(
            PossibleConnectionsService possibleConnectionsService)
        {
            this.possibleConnectionsService = possibleConnectionsService;
        }

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
            var possibleConnections = possibleConnectionsService
                .GetPossibleConnections(vertex.Connections, search.StartTime);
            var labels = new List<Label>();
            foreach (var conn in possibleConnections)
            {
                var label = CreateLabel(conn.EndVertex, conn);
                labels.Add(label);
            }
            return labels;
        }

        public Label CreateLabel(Label currentLabel, Connection connection)
        {
            return new Label
            {

            };
        }
    }
}
