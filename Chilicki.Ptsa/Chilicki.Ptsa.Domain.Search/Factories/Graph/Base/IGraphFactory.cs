using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base
{
    public interface IGraphFactory<TGraph>
    {
        Task<TGraph> CreateGraph(IEnumerable<Stop> stops);
    }
}