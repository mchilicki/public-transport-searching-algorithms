using Chilicki.Ptsa.Domain.InputSearches;
using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.InputSearches
{
    public static class CurrentInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = PoznanInputSearches.Searches;
    }
}
