using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base
{
    public interface IGraphFactory<TGraph>
    {
        TGraph CreateGraph(IEnumerable<Stop> stops, TimeSpan timeSpan);
        void FillVerticesWithSimilarVertices(
            Graph graph, IEnumerable<Stop> stops);
    }
}