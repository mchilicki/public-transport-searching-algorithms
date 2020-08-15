using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Search.Benchmarks.InputSearches
{
    public static class BenchmarkInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("8fda5d8f-e959-4aad-9d64-8e857beb313e"), DestinationStopId = new Guid("87e7a7eb-7bf1-4df0-8ed3-62b845bd535d"), StartTime = TimeSpan.Parse("09:00:00") },
            new SearchInputDto() { StartStopId = new Guid("f88dc322-e038-45c4-8af2-4a6be7f0664b"), DestinationStopId = new Guid("1f8dd008-3940-4630-96d5-b48889feefb5"), StartTime = TimeSpan.Parse("15:00:00") },
            new SearchInputDto() { StartStopId = new Guid("666d08c1-42f8-424c-9e78-df929600f4b4"), DestinationStopId = new Guid("7e8363b2-4e6a-45bc-b276-72bfc2279cf7"), StartTime = TimeSpan.Parse("14:43:00") },
            new SearchInputDto() { StartStopId = new Guid("50d19f91-7223-4603-9c6a-500847b42e30"), DestinationStopId = new Guid("3817248e-ffd1-48d8-818b-0ed9ca3bde47"), StartTime = TimeSpan.Parse("10:22:00") },
            new SearchInputDto() { StartStopId = new Guid("c9835234-011e-4719-b5da-8070864a54ff"), DestinationStopId = new Guid("568c9b9e-2739-4c86-a57c-3ad44a8bcea5"), StartTime = TimeSpan.Parse("11:45:00") },
            new SearchInputDto() { StartStopId = new Guid("9596e80a-4ecf-493d-b12b-39de57e19c47"), DestinationStopId = new Guid("137ea6a3-ac16-489b-9f79-6f90dc2105b9"), StartTime = TimeSpan.Parse("09:12:00") },
            new SearchInputDto() { StartStopId = new Guid("8ec94b4c-8a31-4363-a3e0-a4c8847db32f"), DestinationStopId = new Guid("69695961-eae8-4d53-93d5-d3546d5d082a"), StartTime = TimeSpan.Parse("18:29:00") },
            new SearchInputDto() { StartStopId = new Guid("bb87863a-7575-44f1-9b1c-3f0c4925416c"), DestinationStopId = new Guid("e9d2bd19-065b-46a8-9a18-097fdb430133"), StartTime = TimeSpan.Parse("14:00:00") },
            new SearchInputDto() { StartStopId = new Guid("5df78827-9fcd-48c8-923a-fab4cd67bb1e"), DestinationStopId = new Guid("3410684c-9f13-48fe-a3d3-4251d33ec579"), StartTime = TimeSpan.Parse("10:07:00") },
            new SearchInputDto() { StartStopId = new Guid("03044d8c-6123-4984-ba09-eb5cd7b3aa84"), DestinationStopId = new Guid("c7b255d1-d63e-4f3a-8dd6-1e37aecd698a"), StartTime = TimeSpan.Parse("17:40:00") },
            new SearchInputDto() { StartStopId = new Guid("d644f7bc-0694-4ea8-bfa0-f9633b6a7d13"), DestinationStopId = new Guid("3f186a78-c39b-4bf3-b99f-031f5c9e57db"), StartTime = TimeSpan.Parse("13:12:00") },
            new SearchInputDto() { StartStopId = new Guid("785b246c-6d4b-47db-8bce-32b6d00a1d95"), DestinationStopId = new Guid("72fd58eb-e664-42b5-bf4b-6956d77deab9"), StartTime = TimeSpan.Parse("22:53:00") },
            new SearchInputDto() { StartStopId = new Guid("38b1b1f5-69b9-4859-a8b3-80b048ce7eba"), DestinationStopId = new Guid("8f62443d-1a2f-4630-a5cd-3f3bd24b669a"), StartTime = TimeSpan.Parse("20:57:00") },
            new SearchInputDto() { StartStopId = new Guid("dbedc750-bdee-4ca5-b770-2daf9e5926c2"), DestinationStopId = new Guid("44716b22-5619-4fdb-b09a-9e42e49f1403"), StartTime = TimeSpan.Parse("14:12:00") },
            new SearchInputDto() { StartStopId = new Guid("c5f45fd5-2370-4302-9861-fbb935e082c5"), DestinationStopId = new Guid("4df35c38-107a-455f-ab49-badae2d56a84"), StartTime = TimeSpan.Parse("16:17:00") },
            new SearchInputDto() { StartStopId = new Guid("f6a25faa-91b9-420f-805e-6ab0ef7ac67b"), DestinationStopId = new Guid("ca2a35c8-a6dc-4e00-bb77-8eaaabe39550"), StartTime = TimeSpan.Parse("09:42:00") },
            new SearchInputDto() { StartStopId = new Guid("31e7a37a-0bce-4ffb-906b-74b9725f8b19"), DestinationStopId = new Guid("253350b9-4851-4aff-aee9-b3473a75b215"), StartTime = TimeSpan.Parse("01:28:00") },
            new SearchInputDto() { StartStopId = new Guid("e711370d-85ae-41dd-bb15-a9cee0b87e94"), DestinationStopId = new Guid("1305061d-e066-4631-81f1-bcafe56aeffa"), StartTime = TimeSpan.Parse("18:10:00") },
            new SearchInputDto() { StartStopId = new Guid("d2bcae37-9cda-4626-8dcb-0038428d22ed"), DestinationStopId = new Guid("352ae571-d799-4312-a049-5a71ca2b1a52"), StartTime = TimeSpan.Parse("22:13:00") },
            new SearchInputDto() { StartStopId = new Guid("655c0ac3-3dbc-420f-bb4d-35e00bd18510"), DestinationStopId = new Guid("ee77f252-dc0c-478f-a273-2ac7833b34e5"), StartTime = TimeSpan.Parse("16:57:00") },
            new SearchInputDto() { StartStopId = new Guid("49d8226d-6e11-48c8-85ab-2825f0878131"), DestinationStopId = new Guid("2e9d7282-70fd-491e-bec2-8bc67332009d"), StartTime = TimeSpan.Parse("13:27:00") },
            new SearchInputDto() { StartStopId = new Guid("076371ad-926d-481b-b86b-e9dafcad738e"), DestinationStopId = new Guid("556d054e-5aa5-4e76-834c-d01dc7d63727"), StartTime = TimeSpan.Parse("19:36:00") },
            new SearchInputDto() { StartStopId = new Guid("2e77a266-f1b9-4991-aa85-0701c90a1202"), DestinationStopId = new Guid("4bcde6d6-153c-4b43-b230-091416384c3d"), StartTime = TimeSpan.Parse("11:22:00") },
            new SearchInputDto() { StartStopId = new Guid("b9eae14d-86af-4125-8597-fad973595082"), DestinationStopId = new Guid("b935e4be-01d8-49f5-a829-ba12d2e6c26c"), StartTime = TimeSpan.Parse("08:29:00") },
            new SearchInputDto() { StartStopId = new Guid("ca2e7abb-29c2-4d42-8895-223cb43e73f1"), DestinationStopId = new Guid("45c6b18a-9aad-443f-917b-76a409a4a917"), StartTime = TimeSpan.Parse("07:57:00") },
        };
    }
}
