using Chilicki.Ptsa.Domain.Search.Resources;
using System;

namespace Chilicki.Ptsa.Domain.Search.Helpers.Exceptions
{
    public class NoFastestPathExistsException : Exception
    {
        public NoFastestPathExistsException() : base(SearchResources.NoFastestPathExists)
        {

        }
    }
}
