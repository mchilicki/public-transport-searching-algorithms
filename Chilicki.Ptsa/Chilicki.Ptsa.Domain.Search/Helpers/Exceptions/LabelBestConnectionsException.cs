using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Helpers.Exceptions
{
    public class LabelBestConnectionsException : Exception
    {
        public LabelBestConnectionsException(string message) : base(message)
        {
        }
    }
}
