using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Aggregates.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class BestPathResolver
    {
        private const int ENOUGH_PATHS = 5;
        private readonly FastestPathTransferService transferService;
        private readonly FastestPathFlattener flattener;
        private readonly CurrentConnectionService connectionService;

        public BestPathResolver(
            FastestPathTransferService transferService,
            FastestPathFlattener flattener,
            CurrentConnectionService currentConnectionService)
        {
            this.transferService = transferService;
            this.flattener = flattener;
            this.connectionService = currentConnectionService;
        }

        public ICollection<FastestPath> ResolveBestPaths(
            SearchInput search, BestConnections bestConnections)
        {
            var allPaths = new List<FastestPath>();
            var labels = bestConnections.Find(search.DestinationVertex.Id);
            foreach (var label in labels)
            {
                var pathTreeRoot = ResolveBestPaths(search, bestConnections, label);
                var paths = new List<FastestPath>();
                var path = new List<Connection>();
                RecursivelyFindAllPaths(search, pathTreeRoot, path, paths);
                allPaths.AddRange(paths);
            }
            return allPaths;
        }        

        public TreeNode<Connection> ResolveBestPaths(
            SearchInput search, BestConnections bestConnections, Label label)
        {
            var bestPathsRoot = new TreeNode<Connection>(label.Connection);
            RecursivelyTraverseBestPaths(search, bestConnections, bestPathsRoot);
            return bestPathsRoot;
        }

        private void RecursivelyTraverseBestPaths(
            SearchInput search, BestConnections bestConnections, TreeNode<Connection> currentTreeNode)
        {
            if (IsResolvingNotEnded(search, currentTreeNode.Value))
            {
                var possibleConnections = connectionService
                    .GetPossibleConnections(bestConnections, currentTreeNode.Value);
                foreach (var currentConn in possibleConnections)
                {
                    var nextConn = currentTreeNode.Value;
                    if (transferService.ShouldExtendAlreadyTransfer(currentConn))
                    {
                        transferService.ExtendAlreadyTransfer(currentConn, nextConn);
                    }
                    currentTreeNode.AddChild(currentConn);
                }
                foreach (var child in currentTreeNode.Children)
                {
                    RecursivelyTraverseBestPaths(search, bestConnections, child);
                }
            }            
        }

        private bool IsResolvingNotEnded(SearchInput search, Connection currentConn)
        {
            return search.StartStop.Id != currentConn.StartVertex.Stop.Id;
        }

        private void RecursivelyFindAllPaths(
            SearchInput search,
            TreeNode<Connection> currentNode,
            ICollection<Connection> path,
            ICollection<FastestPath> paths)
        {
            if (IsEnoughPaths(paths))
                return;
            path.Add(currentNode.Value);
            if (!currentNode.Children.Any())
            {
                var reversedPath = path.Reverse();
                var flattenPath = flattener.FlattenFastestPath(reversedPath.ToList());
                paths.Add(FastestPath.Create(search, reversedPath, flattenPath));
            }
            else
            {
                foreach (var child in currentNode.Children)
                {
                    RecursivelyFindAllPaths(search, child, new List<Connection>(path), paths);
                }
            }
        }

        private bool IsEnoughPaths(ICollection<FastestPath> paths)
        {
            return paths.Count() == ENOUGH_PATHS;
        }
    }
}
