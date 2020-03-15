using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Helpers.Exceptions
{
    public class FastestConnectionsException : Exception
    {
        public FastestConnectionsException(string message) : base(message)
        {
        }
    }
}
