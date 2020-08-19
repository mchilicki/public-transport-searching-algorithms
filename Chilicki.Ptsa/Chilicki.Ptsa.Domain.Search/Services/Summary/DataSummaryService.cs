using Chilicki.Ptsa.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.Summary
{
    public class DataSummaryService
    {
        private readonly AgencyRepository agencyRepository;
        private readonly GraphRepository graphRepository;
        private readonly RouteRepository routeRepository;
        private readonly TripRepository tripRepository;
        private readonly VertexRepository vertexRepository;
        private readonly ConnectionRepository connectionRepository;

        public DataSummaryService(
            AgencyRepository agencyRepository,
            GraphRepository graphRepository,
            RouteRepository routeRepository,
            TripRepository tripRepository,
            VertexRepository vertexRepository,
            ConnectionRepository connectionRepository)
        {
            this.agencyRepository = agencyRepository;
            this.graphRepository = graphRepository;
            this.routeRepository = routeRepository;
            this.tripRepository = tripRepository;
            this.vertexRepository = vertexRepository;
            this.connectionRepository = connectionRepository;
        }

        public async Task Summarize()
        {
            var graph = await graphRepository.GetGraph();
            var routes = await routeRepository.GetAllWithTripsAsync();

            var agencies = await agencyRepository.GetAllAsync();
            var agencyName = agencies.FirstOrDefault().Name;

            int vertexCount = graph.Vertices.Count;
            int connectionCount = graph.Vertices.Sum(p => p.Connections.Count);
            int similarVertexCount = graph.Vertices.Sum(p => p.SimilarVertices.Count);
            int routeCount = routes.Count;
            int tripCount = await tripRepository.GetCountAsync();

            double averageTripsPerRouteCount = routes.Average(p => p.Trips.Count);
            int minTripsPerRouteCount = routes
                .Where(p => p.Trips.Any())
                .Min(p => p.Trips.Count);
            int maxTripsPerRouteCount = routes.Max(p => p.Trips.Count);

            double averageConnectionsPerVertexCount = graph.Vertices.Average(p => p.Connections.Count);
            int minConnectionsPerVertexCount = graph.Vertices
                .Where(p => p.Connections.Any())
                .Min(p => p.Connections.Count);
            int maxConnectionsPerVertexCount = graph.Vertices.Max(p => p.Connections.Count);

            double averageSimilarConnectionsPerVertexCount = graph.Vertices.Average(p => p.SimilarVertices.Count);
            int minSimilarConnectionsPerVertexCount = graph.Vertices
                .Where(p => p.SimilarVertices.Any())
                .Min(p => p.SimilarVertices.Count);
            int maxSimilarConnectionsPerVertexCount = graph.Vertices.Max(p => p.SimilarVertices.Count);

            var sb = new StringBuilder();
            sb.AppendLine($"Agency {agencyName}");
            AppendLine(sb, "VertexCount", vertexCount);
            AppendLine(sb, "ConnectionCount", connectionCount);
            AppendLine(sb, "SimilarVertexCount", similarVertexCount);
            AppendLine(sb, "RouteCount", routeCount);
            AppendLine(sb, "TripCount", tripCount);

            sb.AppendLine();
            AppendLine(sb, "AverageTripsPerRouteCount", averageTripsPerRouteCount);
            AppendLine(sb, "MinTripsPerRouteCount", minTripsPerRouteCount);
            AppendLine(sb, "MaxTripsPerRouteCount", maxTripsPerRouteCount);

            sb.AppendLine();
            AppendLine(sb, "AverageConnectionsPerVertexCount", averageConnectionsPerVertexCount);
            AppendLine(sb, "MinConnectionsPerVertexCount", minConnectionsPerVertexCount);
            AppendLine(sb, "MaxConnectionsPerVertexCount", maxConnectionsPerVertexCount);

            sb.AppendLine();
            AppendLine(sb, "AverageSimilarConnectionsPerVertexCount", averageSimilarConnectionsPerVertexCount);
            AppendLine(sb, "MinSimilarConnectionsPerVertexCount", minSimilarConnectionsPerVertexCount);
            AppendLine(sb, "MaxSimilarConnectionsPerVertexCount", maxSimilarConnectionsPerVertexCount);
            string summary = sb.ToString();
            File.WriteAllText($"Summaries\\Summary-{DateTime.Now.ToShortDateString()}.txt", summary);
        }

        private void AppendLine(StringBuilder sb, string name, int count)
        {
            sb.Append($"{name} = {count}{Environment.NewLine}");
        }

        private void AppendLine(StringBuilder sb, string name, double count)
        {
            sb.Append($"{name} = {count}{Environment.NewLine}");
        }
    }
}
