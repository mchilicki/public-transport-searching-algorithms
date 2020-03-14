using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class GraphRepository : BaseRepository<Graph>
    {
        public GraphRepository(DbContext context) : base(context)
        {
        }

        public async Task<Graph> GetGraph()
        {
            var graph = await entities
                .Include(p => p.Vertices)
                    .ThenInclude(v => v.Connections)
                .Include(v => v.Vertices)
                    .ThenInclude(v => v.SimilarVertices)
                .FirstOrDefaultAsync();
            ValidateGraph(graph);       
            return graph;
        }

        private void ValidateGraph(Graph graph)
        {
            if (graph == null && !graph.Vertices.Any() && !graph.Connections.Any())
                throw new InvalidOperationException("There is no created graph for searching. Create graph first, then search fastest connections");
        }
    }
}
