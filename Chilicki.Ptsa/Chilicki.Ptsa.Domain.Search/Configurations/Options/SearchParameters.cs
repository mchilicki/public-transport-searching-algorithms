using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Configurations.Options
{
    public class SearchParameters
    {
        public int MaxTimeAheadFetchingPossibleConnections { get; set; }
        public int MinimumPossibleConnectionsFetched { get; set; }
        public int MinimalTransferTime { get; set; }
        public int MaximalTransferDistanceInMinutes { get; set; }
    }
}
