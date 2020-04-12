using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion
{
    public class MultipleCriterionGraphService : GraphService
    {
        readonly ConnectionFactory connFactory;

        public MultipleCriterionGraphService(
            ConnectionFactory connFactory)
        {
            this.connFactory = connFactory;
        }

        //public BestConnections SetTransferLabelsToSimilarVertices(
        //    BestConnections bestConnections, 
        //    Vertex vertex, 
        //    ICollection<SimilarVertex> similarVertices,
        //    LabelPriorityQueue priorityQueue)
        //{
        //    var vertexLabels = bestConnections.Find(vertex.Id);
        //    foreach (var similarVertex in similarVertices)
        //    {
        //        var departureTime = vertexLabels.First().Connection.DepartureTime;
        //        var conn = connFactory.CreateZeroCostTransfer(
        //            vertex, similarVertex.Similar, departureTime);
        //        var label = 
        //    }
        //    return bestConnections;
        //}
    }
}
