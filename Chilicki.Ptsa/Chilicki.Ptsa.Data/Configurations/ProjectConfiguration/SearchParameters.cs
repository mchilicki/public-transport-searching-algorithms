using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public class SearchParameters
    {
        public int MaxTimeAheadFetchingPossibleConnections { get; set; }
        public int MinimumPossibleConnectionsFetched { get; set; }
        public int MinimalTransferInMinutes { get; set; }
        public int MaximalTransferInMinutes { get; set; }
    }
}
