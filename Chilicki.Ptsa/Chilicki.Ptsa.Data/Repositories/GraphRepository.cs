using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class GraphRepository : BaseRepository<Graph>, IBaseRepository<Graph>
    {
        public GraphRepository(DbContext context) : base(context)
        {
        }

        public async Task<Graph> GetGraph()
        {
            var graph = await entities.FirstOrDefaultAsync();
            ValidateGraph(graph);       
            return graph;
        }

        private void ValidateGraph(Graph graph)
        {
            if (graph == null)
                throw new InvalidOperationException("There is no created graph for searching. Create graph first, then search fastest connections");
        }
    }
}
