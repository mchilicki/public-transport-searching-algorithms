using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion
{
    public class LabelFactory
    {
        private readonly PossibleConnectionsService connectionsService;
        private readonly ConnectionTimeCalculator calculator;
        private readonly TransferCalculator transferCalculator;

        public LabelFactory(
            PossibleConnectionsService connectionsService,
            ConnectionTimeCalculator calculator, 
            TransferCalculator transferCalculator)
        {
            this.connectionsService = connectionsService;
            this.calculator = calculator;
            this.transferCalculator = transferCalculator;
        }

        public ICollection<Label> CreateEmptyLabels()
        {
            return new List<Label>();
        }   

        public ICollection<Label> CreateStartLabels(
            Vertex vertex, SearchInput search)
        {
            var possibleConnections = connectionsService
                .GetPossibleConnections(vertex.Connections, search.StartTime);
            var labels = new List<Label>();
            foreach (var conn in possibleConnections)
            {
                var label = CreateStartLabel(conn);
                labels.Add(label);
            }
            return labels;
        }

        public Label CreateStartLabel(Connection connection)
        {
            var connectionTime = calculator.CalculateConnectionTime(connection);
            return new Label
            {
                Vertex = connection.EndVertex,
                Connection = connection,
                TimeCriterion = Criterion.Create(connectionTime),
                StopCountCriterion = Criterion.CreateOne,
                TransferCriterion = Criterion.CreateZero,
            };
        }

        public Label CreateLabel(Label currentLabel, Connection connection)
        {
            int allConnectionsTime = calculator.CalculateAllConnectionsTime(currentLabel, connection);
            return new Label
            {
                Vertex = connection.EndVertex,
                Connection = connection,
                TimeCriterion = Criterion.Create(allConnectionsTime),
                StopCountCriterion = Criterion.CreateOneMore(currentLabel.StopCountCriterion),
                TransferCriterion = transferCalculator.CalculateTransferCriterion(currentLabel, connection)
            };
        }        
    }
}
