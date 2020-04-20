using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
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
        private readonly ConnectionFactory connectionFactory;

        public LabelFactory(
            PossibleConnectionsService connectionsService,
            ConnectionTimeCalculator calculator, 
            TransferCalculator transferCalculator,
            ConnectionFactory connectionFactory)
        {
            this.connectionsService = connectionsService;
            this.calculator = calculator;
            this.transferCalculator = transferCalculator;
            this.connectionFactory = connectionFactory;
        }

        public ICollection<Label> CreateEmptyLabels()
        {
            return new List<Label>();
        }   

        public ICollection<Label> CreateStartLabels(
            Vertex vertex, SearchInput search)
        {
            //var possibleConnections = connectionsService
            //    .GetPossibleConnections(vertex.Connections, search.StartTime);
            var labels = new List<Label>();
            var conn = connectionFactory.CreateSameVertexZeroCostTransfer(
                vertex, search.StartTime);
            CreateLabelAndAddToLabelList(labels, conn);
            //foreach (var possibleConn in possibleConnections)
            //{                
            //    CreateLabelAndAddToLabelList(labels, possibleConn);
            //}          
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
                TransferCriterion = transferCalculator.CalculateTransferCriterion(currentLabel, connection)
            };
        }

        private void CreateLabelAndAddToLabelList(
            ICollection<Label> labels, Connection conn)
        {
            var label = CreateStartLabel(conn);
            labels.Add(label);
        }
    }
}
