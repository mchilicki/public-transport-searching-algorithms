using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Search.Benchmarks.InputSearches
{
    public static class LahtiInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("c94833e9-f049-4117-9a63-e0648caa7d95"), DestinationStopId = new Guid("8661e9b1-2238-4ce4-9cd2-a062dce2705d"), StartTime = TimeSpan.Parse("15:57:00") },
            new SearchInputDto() { StartStopId = new Guid("d6eaf5b8-daaa-4ca8-87ec-bc0c92731015"), DestinationStopId = new Guid("011dd86c-8175-4458-a322-276d1adcc950"), StartTime = TimeSpan.Parse("12:36:00") },
            new SearchInputDto() { StartStopId = new Guid("fbacde67-f0a6-47f3-b108-894fe80dc322"), DestinationStopId = new Guid("fb20876c-a4ea-4302-9c6a-dd59d7018f34"), StartTime = TimeSpan.Parse("08:18:00") },
            new SearchInputDto() { StartStopId = new Guid("72ec2d22-a4bd-4824-ba18-363e71413097"), DestinationStopId = new Guid("5d046c90-a884-428a-9566-f304cc79e832"), StartTime = TimeSpan.Parse("19:40:00") },
            new SearchInputDto() { StartStopId = new Guid("bc410d34-1d74-4beb-b501-daff8e66e4ce"), DestinationStopId = new Guid("606d83ea-5941-441e-bcd5-699185ba4981"), StartTime = TimeSpan.Parse("09:35:00") },
            new SearchInputDto() { StartStopId = new Guid("33bd5386-58a9-4afa-b6eb-ef6a4c2b63e4"), DestinationStopId = new Guid("f8bcadfb-0c4a-46ba-84b8-5b660862ea57"), StartTime = TimeSpan.Parse("20:52:00") },
            new SearchInputDto() { StartStopId = new Guid("5faa2af6-6d86-4553-87b7-1f6b989d667b"), DestinationStopId = new Guid("fbf554f8-1fd7-404a-a4d6-64a07e2ad3b8"), StartTime = TimeSpan.Parse("13:28:00") },
            new SearchInputDto() { StartStopId = new Guid("1632cf62-d8f0-4ed6-927b-f446ab573b9f"), DestinationStopId = new Guid("52ec6040-3796-488c-a00a-5778fe69c08a"), StartTime = TimeSpan.Parse("13:11:00") },
            new SearchInputDto() { StartStopId = new Guid("b32d2800-f1ce-4c11-a2a1-89364bc2f495"), DestinationStopId = new Guid("e8cce830-0c8f-480e-8574-fc81220c9822"), StartTime = TimeSpan.Parse("21:50:00") },
            new SearchInputDto() { StartStopId = new Guid("40bb74c4-4267-45d4-8108-03490b16a524"), DestinationStopId = new Guid("3eaceb2e-bf21-4d49-8abe-7ba51ea09e63"), StartTime = TimeSpan.Parse("15:26:00") },
            new SearchInputDto() { StartStopId = new Guid("afa5c24d-3f53-4c5f-a5ee-d55fc202bcd5"), DestinationStopId = new Guid("f160a373-da84-4909-90e9-8164e08d350c"), StartTime = TimeSpan.Parse("10:02:00") },
            new SearchInputDto() { StartStopId = new Guid("3d539612-3ea7-4cac-9a53-50a2029114e2"), DestinationStopId = new Guid("6c7eaacc-cbb7-4534-a0b2-3c03e56f1d9d"), StartTime = TimeSpan.Parse("19:17:00") },
            new SearchInputDto() { StartStopId = new Guid("fe34fba0-42e7-4fbe-b2a6-b7dbd352dc44"), DestinationStopId = new Guid("d22e27c2-8c0e-4660-b816-015c392dead9"), StartTime = TimeSpan.Parse("21:59:00") },
            new SearchInputDto() { StartStopId = new Guid("56c0b2f6-1d77-48fd-9a8a-79356c5e6cc1"), DestinationStopId = new Guid("12402b9e-4377-4a44-99e8-e76751fd9a6e"), StartTime = TimeSpan.Parse("16:25:00") },
            new SearchInputDto() { StartStopId = new Guid("3adb77ea-182b-43d1-9487-4857a3dda698"), DestinationStopId = new Guid("f8592526-9b2a-430f-8287-8afa639f2d24"), StartTime = TimeSpan.Parse("11:55:00") },
            new SearchInputDto() { StartStopId = new Guid("a2039438-2d35-4af0-af59-b35db7c92e6b"), DestinationStopId = new Guid("be5b346d-a07c-4085-849a-782cf359bf19"), StartTime = TimeSpan.Parse("20:37:00") },
            new SearchInputDto() { StartStopId = new Guid("ca6f8cc1-3ecb-45b7-958f-54ce87e5c156"), DestinationStopId = new Guid("7c4f0410-114f-464a-8eb2-538e02609add"), StartTime = TimeSpan.Parse("18:49:00") },
            new SearchInputDto() { StartStopId = new Guid("e5957a19-82bd-4ed9-88aa-903727a0d558"), DestinationStopId = new Guid("5decf428-40a9-4c87-b4a2-dcc377d04f26"), StartTime = TimeSpan.Parse("13:34:00") },
            new SearchInputDto() { StartStopId = new Guid("963a8e14-eb0d-4446-a5c7-24172a73308f"), DestinationStopId = new Guid("5808c79c-258f-43af-8ef8-ecc12cd9f2fc"), StartTime = TimeSpan.Parse("07:11:00") },
            new SearchInputDto() { StartStopId = new Guid("9337352d-db5e-4b7c-8e34-206fb6a3374f"), DestinationStopId = new Guid("45ea744c-aa10-4ed9-9b25-37f179ad883c"), StartTime = TimeSpan.Parse("19:49:00") },
            new SearchInputDto() { StartStopId = new Guid("16a23483-a258-4728-be4d-5f31315cffab"), DestinationStopId = new Guid("b621ba88-567a-4bd9-9429-8673feb6e9d8"), StartTime = TimeSpan.Parse("09:25:00") },
            new SearchInputDto() { StartStopId = new Guid("d23431f6-0bd8-46cf-88bb-b3cc1093f08f"), DestinationStopId = new Guid("16a23483-a258-4728-be4d-5f31315cffab"), StartTime = TimeSpan.Parse("10:53:00") },
            new SearchInputDto() { StartStopId = new Guid("f7e9373a-ab11-4efd-bef6-68cb4858c329"), DestinationStopId = new Guid("3fe82e82-5242-43a6-879d-8e1e803e919f"), StartTime = TimeSpan.Parse("06:15:00") },
            new SearchInputDto() { StartStopId = new Guid("a79f80f9-b997-4d11-b0ff-458be09816b4"), DestinationStopId = new Guid("9418d727-bb64-4d7a-9b67-39140155bc0b"), StartTime = TimeSpan.Parse("08:11:00") },
            new SearchInputDto() { StartStopId = new Guid("9c160382-e352-4ef1-8175-f51a31345ef0"), DestinationStopId = new Guid("4a8886e0-2c1a-4571-a472-a54a43da2f93"), StartTime = TimeSpan.Parse("11:36:00") },
        };
    }
}
