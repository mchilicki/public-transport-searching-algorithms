using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Search.Benchmarks.InputSearches
{
    public static class PoznanInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("4d1e0b5f-ab3f-4439-b5a7-33ce2c3cb403"), DestinationStopId = new Guid("d051884d-0a6e-4436-82ee-3b4fb2a8971b"), StartTime = TimeSpan.Parse("17:29:00") },
            new SearchInputDto() { StartStopId = new Guid("af82bf46-eaea-4d0b-8782-c2914e50299e"), DestinationStopId = new Guid("4930dcc4-7501-4ba8-93c4-100a9c114e98"), StartTime = TimeSpan.Parse("07:08:00") },
            new SearchInputDto() { StartStopId = new Guid("ff6c07e2-1493-4943-8d91-604e3f5e36fa"), DestinationStopId = new Guid("373e8229-bc42-45f0-86aa-7a4233ecd58e"), StartTime = TimeSpan.Parse("19:55:00") },
            new SearchInputDto() { StartStopId = new Guid("db4b1a0d-9885-4619-a327-0378ac576245"), DestinationStopId = new Guid("8c421db0-d679-48ab-9e10-44c7de93f0bf"), StartTime = TimeSpan.Parse("8:29:00") },
            new SearchInputDto() { StartStopId = new Guid("e3d6670c-9dca-4c75-aca7-b7aef9aed651"), DestinationStopId = new Guid("3d339335-e6aa-494a-b8c0-186cc3f3623a"), StartTime = TimeSpan.Parse("11:41:00") },
            new SearchInputDto() { StartStopId = new Guid("765abf40-d61f-43eb-b669-5c78dd630de2"), DestinationStopId = new Guid("84e35bf9-30a1-437c-aeb4-1e59bb184863"), StartTime = TimeSpan.Parse("16:44:00") },
            new SearchInputDto() { StartStopId = new Guid("fa0769d1-3ba7-4182-8f9c-c66babb5d63d"), DestinationStopId = new Guid("368bd86b-fad7-4d52-a219-2dc1e29dfa80"), StartTime = TimeSpan.Parse("12:43:00") },
            new SearchInputDto() { StartStopId = new Guid("06b32f59-8674-4137-99d5-496e1be80c32"), DestinationStopId = new Guid("c1b4f4a0-5ea3-49a4-bd78-32698c1ec525"), StartTime = TimeSpan.Parse("09:41:00") },
            new SearchInputDto() { StartStopId = new Guid("43bf9a6f-2329-42ff-a019-be8fb8845fb4"), DestinationStopId = new Guid("17c7b927-c945-42b7-8676-f77358196a4c"), StartTime = TimeSpan.Parse("13:35:00") },
            new SearchInputDto() { StartStopId = new Guid("961e9091-f0db-4c75-bf71-030a2d966f2d"), DestinationStopId = new Guid("c67d5949-165a-4b9e-bc33-fc648f77af8b"), StartTime = TimeSpan.Parse("03:11:00") },
            new SearchInputDto() { StartStopId = new Guid("02ac60bb-7856-406f-a672-e007fd65ecb8"), DestinationStopId = new Guid("6835cdb1-a577-4571-9d6f-b49a84910ede"), StartTime = TimeSpan.Parse("10:48:00") },
            new SearchInputDto() { StartStopId = new Guid("6b26b4ba-cba5-467a-821e-895216601fae"), DestinationStopId = new Guid("6d5dc520-5b31-4045-a3f5-082b909d042e"), StartTime = TimeSpan.Parse("12:07:00") },
            new SearchInputDto() { StartStopId = new Guid("850dfcb7-bbd6-4c8b-a7fe-09b68d1861ef"), DestinationStopId = new Guid("25a430cc-88a0-43ab-b4a4-60f582baae9e"), StartTime = TimeSpan.Parse("11:33:00") },
            new SearchInputDto() { StartStopId = new Guid("9730ca62-a026-4350-9fc3-cea1900b936d"), DestinationStopId = new Guid("530b77a2-d391-40cb-bfd7-710d37812bbb"), StartTime = TimeSpan.Parse("15:46:00") },
            new SearchInputDto() { StartStopId = new Guid("f6f29d35-8e84-4480-a434-e2be27935bc1"), DestinationStopId = new Guid("76f5ad65-27da-4a23-ae5a-f05b31869fb7"), StartTime = TimeSpan.Parse("21:10:00") },
            new SearchInputDto() { StartStopId = new Guid("5d64f9f7-f27a-49a0-9f06-8fc2687aa7c7"), DestinationStopId = new Guid("77dfe413-55ad-44ed-9ac8-68d8b2e740b0"), StartTime = TimeSpan.Parse("22:18:00") },
            new SearchInputDto() { StartStopId = new Guid("a2ff76e3-e012-4287-b7e1-05f23a8c4e78"), DestinationStopId = new Guid("535d1a25-e75a-4291-a81b-a37e31948d34"), StartTime = TimeSpan.Parse("22:06:00") },
            new SearchInputDto() { StartStopId = new Guid("159c88a1-403b-48c5-a721-daba6d01b2bc"), DestinationStopId = new Guid("e18c251b-2f3a-4829-afbb-4ea4e0ded316"), StartTime = TimeSpan.Parse("11:16:00") },
            new SearchInputDto() { StartStopId = new Guid("9f0c0e73-12c0-490f-8034-c4db6c6a08ee"), DestinationStopId = new Guid("b2c7463b-d6d7-4d8f-b934-ea58107b8213"), StartTime = TimeSpan.Parse("12:49:00") },
            new SearchInputDto() { StartStopId = new Guid("c353000e-ac95-4f81-8493-51cf484c43a2"), DestinationStopId = new Guid("c43dcc2e-dd98-4b8b-b9eb-030cb466d24b"), StartTime = TimeSpan.Parse("15:22:00") },
            new SearchInputDto() { StartStopId = new Guid("f757c3f7-3b1a-4687-be79-7b3971cde378"), DestinationStopId = new Guid("765abf40-d61f-43eb-b669-5c78dd630de2"), StartTime = TimeSpan.Parse("11:41:00") },
            new SearchInputDto() { StartStopId = new Guid("23534a71-7f7d-4d6a-abc2-b4345517e010"), DestinationStopId = new Guid("d3c809c4-8eb4-4bc7-b20e-25de4ea88e4e"), StartTime = TimeSpan.Parse("13:44:00") },
            new SearchInputDto() { StartStopId = new Guid("659bc0fd-acec-4b30-81ef-a93ae23114c9"), DestinationStopId = new Guid("729dd769-d8ff-4d95-97eb-2dd9b8ceb24f"), StartTime = TimeSpan.Parse("10:36:00") },
            new SearchInputDto() { StartStopId = new Guid("720b324d-0310-411c-828d-869f8b8be06a"), DestinationStopId = new Guid("6d40c1f0-669e-4235-bf17-4f5d802acc0c"), StartTime = TimeSpan.Parse("17:52:00") },
            new SearchInputDto() { StartStopId = new Guid("50126350-1115-492b-9551-380e5a87c59c"), DestinationStopId = new Guid("0e60f929-bdb3-458a-a1ad-f6baa1ea7be3"), StartTime = TimeSpan.Parse("17:43:00") },
        };
    }
}
