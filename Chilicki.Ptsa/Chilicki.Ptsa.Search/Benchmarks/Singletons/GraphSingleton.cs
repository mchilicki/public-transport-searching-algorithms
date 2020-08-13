using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search.Benchmarks.Singletons
{
    public class GraphSingleton
    {
        private GraphSingleton() { }

        private static Graph graph;

        public async static Task<Graph> GetGraph(GraphRepository graphRepository)
        {
            if (graph == null)
            {
                Console.WriteLine("FetchingGraphFromDatabase - Started");
                graph = await graphRepository.GetGraph();
                Console.WriteLine("FetchingGraphFromDatabase - Done");
            }
            Console.WriteLine("ReturningFetchedGraph");
            return graph;
        }
    }
}
