using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.InputSearches
{
    public static class StavangerInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("652b8cd6-3626-46c3-a4f9-f6f3d2c88f99"), DestinationStopId = new Guid("749b1182-3430-4172-93c3-ee4b04ad160b"), StartTime = TimeSpan.Parse("12:49:00") },
            new SearchInputDto() { StartStopId = new Guid("a8b076f9-99c2-48cb-9422-4e2ff0fa1be9"), DestinationStopId = new Guid("749b1182-3430-4172-93c3-ee4b04ad160b"), StartTime = TimeSpan.Parse("11:25:00") },
            new SearchInputDto() { StartStopId = new Guid("1a83f165-6bf3-45f2-82a0-a79303b79458"), DestinationStopId = new Guid("35d755d5-6713-4790-8336-f50c0a9b0a54"), StartTime = TimeSpan.Parse("11:12:00") },
            new SearchInputDto() { StartStopId = new Guid("1c0cba9f-49ba-475e-9551-84141a968bf3"), DestinationStopId = new Guid("e6b5813c-64c9-4285-9343-ac35c6d3c15b"), StartTime = TimeSpan.Parse("16:23:00") },
            new SearchInputDto() { StartStopId = new Guid("a4650e6b-f42e-4aa6-ad6f-e6f104ec1760"), DestinationStopId = new Guid("bfeba4e9-c6dd-4cae-9898-939eacb28c62"), StartTime = TimeSpan.Parse("12:20:00") },
            new SearchInputDto() { StartStopId = new Guid("d7a1163f-8ae7-4413-8e09-53fbf62ae989"), DestinationStopId = new Guid("0325cffc-ac55-4f5c-a685-17e2f40d0f41"), StartTime = TimeSpan.Parse("13:55:00") },
            new SearchInputDto() { StartStopId = new Guid("0aa8c011-ce5e-4efb-a2fc-4214e7f2d4b1"), DestinationStopId = new Guid("13493615-a803-4cd0-85e6-c069b80ce8ea"), StartTime = TimeSpan.Parse("11:03:00") },
            new SearchInputDto() { StartStopId = new Guid("486d5290-5d79-4300-b2f6-bfc9000a21c0"), DestinationStopId = new Guid("2080e829-6cc3-4366-b482-e3ebdcf022cd"), StartTime = TimeSpan.Parse("12:05:00") },
            new SearchInputDto() { StartStopId = new Guid("03a39e08-a7bb-4985-a619-9dc4e4388b6d"), DestinationStopId = new Guid("fa7bde30-5d7a-4a97-bf63-da537ae98c53"), StartTime = TimeSpan.Parse("15:20:00") },
            new SearchInputDto() { StartStopId = new Guid("b2581617-1f02-4353-9b07-7a71833357d5"), DestinationStopId = new Guid("6033891e-e2fc-49a0-8500-f6619b2580ee"), StartTime = TimeSpan.Parse("11:28:00") },
            new SearchInputDto() { StartStopId = new Guid("1a83f165-6bf3-45f2-82a0-a79303b79458"), DestinationStopId = new Guid("07d17c99-aec2-4672-a63d-a97b73394d42"), StartTime = TimeSpan.Parse("12:18:00") },
            new SearchInputDto() { StartStopId = new Guid("b7341e56-fff7-446a-b9e0-42e1e5d784e1"), DestinationStopId = new Guid("5dbd3744-0949-4011-9e53-6f728c2722b6"), StartTime = TimeSpan.Parse("16:41:00") },
            new SearchInputDto() { StartStopId = new Guid("2219ec3c-8282-46c3-aeee-22867046d12b"), DestinationStopId = new Guid("11845fbe-50ed-4e36-b854-c620e7f7d41a"), StartTime = TimeSpan.Parse("13:34:00") },
            new SearchInputDto() { StartStopId = new Guid("343f6fc2-1798-4f34-91db-4edd772a4557"), DestinationStopId = new Guid("07bba523-af43-48fe-a401-7c8e53a6104d"), StartTime = TimeSpan.Parse("11:53:00") },
            new SearchInputDto() { StartStopId = new Guid("9cc74620-3edd-4c12-8f9e-67fd65127ea1"), DestinationStopId = new Guid("422b44f1-417e-4ccb-9da8-650861175154"), StartTime = TimeSpan.Parse("13:59:00") },
            new SearchInputDto() { StartStopId = new Guid("7568bc47-74c7-4179-9514-80bc423b7b8e"), DestinationStopId = new Guid("b32fe999-d51b-4e12-825d-2c8551d56658"), StartTime = TimeSpan.Parse("12:50:00") },
            new SearchInputDto() { StartStopId = new Guid("a4afab3b-e995-47b2-89ef-90bd3d87a3fa"), DestinationStopId = new Guid("35d755d5-6713-4790-8336-f50c0a9b0a54"), StartTime = TimeSpan.Parse("15:22:00") },
            new SearchInputDto() { StartStopId = new Guid("6b9fc373-6934-4c4c-b05d-5d054d2e036e"), DestinationStopId = new Guid("41949c93-ab6a-452a-9ab3-e1ad24e91cdf"), StartTime = TimeSpan.Parse("15:45:00") },
            new SearchInputDto() { StartStopId = new Guid("a93a4de9-11a8-4011-8bdf-dcfbd874f322"), DestinationStopId = new Guid("dc98dfa3-812b-4f05-adef-e473539a407b"), StartTime = TimeSpan.Parse("10:24:00") },
            new SearchInputDto() { StartStopId = new Guid("bedfc46d-72d4-4c72-902d-1f5127f4685a"), DestinationStopId = new Guid("1b21ca89-503f-4a73-99f6-b26afae08786"), StartTime = TimeSpan.Parse("10:41:00") },
            new SearchInputDto() { StartStopId = new Guid("b2581617-1f02-4353-9b07-7a71833357d5"), DestinationStopId = new Guid("3168cbaf-3681-4692-9015-3a73864fadb2"), StartTime = TimeSpan.Parse("16:58:00") },
            new SearchInputDto() { StartStopId = new Guid("652b8cd6-3626-46c3-a4f9-f6f3d2c88f99"), DestinationStopId = new Guid("fa7bde30-5d7a-4a97-bf63-da537ae98c53"), StartTime = TimeSpan.Parse("10:04:00") },
            new SearchInputDto() { StartStopId = new Guid("36229a89-f872-4c16-964e-9745cc97fd39"), DestinationStopId = new Guid("9c6fbabc-d3db-4449-8c55-b6a079ead261"), StartTime = TimeSpan.Parse("11:05:00") },
            new SearchInputDto() { StartStopId = new Guid("a3a1f874-74f0-4770-b3cf-69a81a32397a"), DestinationStopId = new Guid("41a21d48-4eab-4c65-9933-31d6f4bd6c63"), StartTime = TimeSpan.Parse("12:21:00") },
            new SearchInputDto() { StartStopId = new Guid("117c5111-de2d-483c-b7ec-0bcfdead1274"), DestinationStopId = new Guid("8877eccc-ceee-43e2-8618-f0888246153b"), StartTime = TimeSpan.Parse("11:59:00") },
        };
    }
}
