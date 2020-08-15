using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Search.Benchmarks.InputSearches
{
    public static class StavangerInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("15bc479b-c63b-45a2-9b61-e5e8e1cf92b7"), DestinationStopId = new Guid("135896ba-a15b-47fb-a285-2061e31964e0"), StartTime = TimeSpan.Parse("10:07:00") },
            new SearchInputDto() { StartStopId = new Guid("e94f80ef-ec65-4471-b29a-498616ff5453"), DestinationStopId = new Guid("0a129d9c-0e53-4d7a-afdc-7d9b799ea8d6"), StartTime = TimeSpan.Parse("09:37:00") },
            new SearchInputDto() { StartStopId = new Guid("6552222f-e17d-401c-952d-ba327d9c0676"), DestinationStopId = new Guid("161eadab-f9f6-4613-aa13-edbbd797082d"), StartTime = TimeSpan.Parse("06:49:00") },
            new SearchInputDto() { StartStopId = new Guid("53c23042-bd6b-4760-8fbd-3d4a8def8cf6"), DestinationStopId = new Guid("6b2bbcca-ab6f-4204-a38a-4d6a9e23d83b"), StartTime = TimeSpan.Parse("16:47:00") },
            new SearchInputDto() { StartStopId = new Guid("d6ff5859-18d4-4e06-873c-b17c1ef0f5eb"), DestinationStopId = new Guid("4df9fc21-b0f0-4a69-88d3-018ed7ad45ee"), StartTime = TimeSpan.Parse("10:36:00") },
            new SearchInputDto() { StartStopId = new Guid("da016371-1ecf-4453-ad48-90e7fc56c6e7"), DestinationStopId = new Guid("14825b97-fa8d-451e-abaa-77c4a401fa28"), StartTime = TimeSpan.Parse("18:34:00") },
            new SearchInputDto() { StartStopId = new Guid("ca1694b1-5117-428f-8f13-ee08a7b11ac9"), DestinationStopId = new Guid("3cd100ad-bc8a-4531-84b5-df8f1fcb621b"), StartTime = TimeSpan.Parse("17:49:00") },
            new SearchInputDto() { StartStopId = new Guid("0716b525-1165-445b-87e9-f6ed8a773a99"), DestinationStopId = new Guid("f820c6fb-ee58-4c5c-afd2-8c73017de28a"), StartTime = TimeSpan.Parse("02:04:00") },
            new SearchInputDto() { StartStopId = new Guid("2a77731e-da60-4c14-b0e1-c4944da0fcdd"), DestinationStopId = new Guid("2bb539a5-4a4c-4d64-9ac9-d048113c1a35"), StartTime = TimeSpan.Parse("15:01:00") },
            new SearchInputDto() { StartStopId = new Guid("7033d22b-6253-45d5-8b29-a799394a900f"), DestinationStopId = new Guid("225ca602-d2f0-411e-acac-2db41f072b98"), StartTime = TimeSpan.Parse("20:15:00") },
            new SearchInputDto() { StartStopId = new Guid("446ba061-8b40-40e5-8561-8f3605619fdb"), DestinationStopId = new Guid("861659b0-cd32-4343-9eae-efa3cb3c841a"), StartTime = TimeSpan.Parse("10:49:00") },
            new SearchInputDto() { StartStopId = new Guid("a7a4bcd8-70fb-4f91-ab91-ba33caf9a17b"), DestinationStopId = new Guid("eae0fb61-e55d-4b7e-a576-28d43d27c29b"), StartTime = TimeSpan.Parse("04:17:00") },
            new SearchInputDto() { StartStopId = new Guid("f8275110-e276-49a0-86ad-59e8fac2bc7b"), DestinationStopId = new Guid("e6eb9c68-2481-4654-bdb5-5a602002206a"), StartTime = TimeSpan.Parse("15:59:00") },
            new SearchInputDto() { StartStopId = new Guid("2f94a10e-50c9-4dc0-8a5f-d2dcdd7531f8"), DestinationStopId = new Guid("27d7ee30-88da-421b-a83f-e5efe3a511a0"), StartTime = TimeSpan.Parse("19:19:00") },
            new SearchInputDto() { StartStopId = new Guid("a006e173-5ea7-459e-bcda-7c38edbcb94c"), DestinationStopId = new Guid("8c8ffb2f-01a8-4a02-bb8a-89f47472e684"), StartTime = TimeSpan.Parse("05:57:00") },
            new SearchInputDto() { StartStopId = new Guid("a82bd2b5-6e4e-4836-b265-c251af032970"), DestinationStopId = new Guid("0a3b7927-f8b7-4741-a15a-a37825f46e2a"), StartTime = TimeSpan.Parse("12:41:00") },
            new SearchInputDto() { StartStopId = new Guid("ae21bd0b-860e-4414-a9dc-3ed6db9b3408"), DestinationStopId = new Guid("a7a4bcd8-70fb-4f91-ab91-ba33caf9a17b"), StartTime = TimeSpan.Parse("14:13:00") },
            new SearchInputDto() { StartStopId = new Guid("574a08e6-c78f-4258-bfd8-d88230f9360a"), DestinationStopId = new Guid("2a46cec8-bf69-44d7-aad3-45304c25621a"), StartTime = TimeSpan.Parse("13:01:00") },
            new SearchInputDto() { StartStopId = new Guid("c5c8c5c6-7eab-432b-b5f7-41fb9d5997ac"), DestinationStopId = new Guid("e59e5169-7987-4474-a8b0-ee8a30725f1c"), StartTime = TimeSpan.Parse("14:02:00") },
            new SearchInputDto() { StartStopId = new Guid("8caec67d-de04-42e7-8247-212ea53aecb0"), DestinationStopId = new Guid("2dc3ced2-ca87-4859-bbc0-873810d6584a"), StartTime = TimeSpan.Parse("16:08:00") },
            new SearchInputDto() { StartStopId = new Guid("ae033169-6eb8-4f16-8d00-26c57a3d8eeb"), DestinationStopId = new Guid("1ac099d3-b6b1-48f9-bb58-128593fb93b7"), StartTime = TimeSpan.Parse("17:56:00") },
            new SearchInputDto() { StartStopId = new Guid("65d137a2-a6dc-45ee-b147-73842d4cf051"), DestinationStopId = new Guid("ac727587-70b1-4a69-9e87-b1df0d14bbde"), StartTime = TimeSpan.Parse("08:36:00") },
            new SearchInputDto() { StartStopId = new Guid("39cecfe7-6867-4e41-84cf-4225be0c6b48"), DestinationStopId = new Guid("006c654d-1313-48a7-a930-f745d736b41a"), StartTime = TimeSpan.Parse("20:20:00") },
            new SearchInputDto() { StartStopId = new Guid("4eeb8e70-e4ff-4d77-b947-4614290860d3"), DestinationStopId = new Guid("1e6aa22c-76d1-405c-a868-3935cb552838"), StartTime = TimeSpan.Parse("02:44:00") },
            new SearchInputDto() { StartStopId = new Guid("41a21d48-4eab-4c65-9933-31d6f4bd6c63"), DestinationStopId = new Guid("ea7fef79-a95f-4aab-b2b4-1b38c045c92a"), StartTime = TimeSpan.Parse("19:26:00") },
        };
    }
}
