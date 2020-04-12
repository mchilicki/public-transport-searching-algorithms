using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion
{
    public class BestConnections
    {
        public IDictionary<Guid, ICollection<Label>> Dictionary { get; } 
            = new Dictionary<Guid, ICollection<Label>>();

        public void Add(Guid vertexId, ICollection<Label> labels)
        {
            Dictionary.Add(vertexId, labels);
        }

        public ICollection<Label> Find(Guid vertexId)
        {
            Dictionary.TryGetValue(vertexId, out ICollection<Label> labels);
            return labels;
        }

        public ICollection<Label> Get(Guid? vertexId)
        {
            if (!vertexId.HasValue)
            {
                throw new LabelBestConnectionsException("Null vertexId in dictionary Find");
            }
            return Find(vertexId.Value);
        }
    }
}
