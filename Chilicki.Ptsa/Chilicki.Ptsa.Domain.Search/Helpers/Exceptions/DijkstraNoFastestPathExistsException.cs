using Chilicki.Ptsa.Domain.Search.Resources;
using System;

namespace Chilicki.Ptsa.Domain.Search.Helpers.Exceptions
{
    public class DijkstraNoFastestPathExistsException : Exception
    {
        public DijkstraNoFastestPathExistsException() : base(SearchResources.NoFastestPathExists)
        {

        }
    }
}
