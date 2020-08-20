using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.InputSearches
{
    public static class PoznanInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("00fc3e81-4d18-4a41-a5b3-89f31743f25c"), DestinationStopId = new Guid("5c690eb3-8bf5-4f83-a33c-64376b7b5d5b"), StartTime = TimeSpan.Parse("12:51:00") },
            new SearchInputDto() { StartStopId = new Guid("15f47d7d-8943-49dd-b0a5-10d67295df9d"), DestinationStopId = new Guid("d81e98ec-6163-4fd2-9f82-76c6845f4c78"), StartTime = TimeSpan.Parse("15:39:00") },
            new SearchInputDto() { StartStopId = new Guid("cecc1a27-2b97-4d1e-ae5e-c181f16b2977"), DestinationStopId = new Guid("45b6a855-b97e-4141-89d5-c034cf275092"), StartTime = TimeSpan.Parse("14:59:00") },
            new SearchInputDto() { StartStopId = new Guid("889571c5-8cda-4fb5-bdc9-349e9523a99b"), DestinationStopId = new Guid("f5366b45-8aa7-4935-8e53-f8be7c33fa65"), StartTime = TimeSpan.Parse("12:37:00") },
            new SearchInputDto() { StartStopId = new Guid("b653b824-31bc-4618-b31e-b41407dfc472"), DestinationStopId = new Guid("18873a65-7d4b-44c6-8490-a1cdd34e5811"), StartTime = TimeSpan.Parse("10:09:00") },
            new SearchInputDto() { StartStopId = new Guid("b7bd1442-8f80-45bf-a73b-c8ae87c30f9d"), DestinationStopId = new Guid("e9d20980-5aad-4b8d-bca0-468a64f55471"), StartTime = TimeSpan.Parse("13:32:00") },
            new SearchInputDto() { StartStopId = new Guid("14be4fa1-820c-49b6-8b2d-8eb56bc3a84b"), DestinationStopId = new Guid("0fdbdee7-ec08-4d6d-8b7f-b78d2819d847"), StartTime = TimeSpan.Parse("15:26:00") },
            new SearchInputDto() { StartStopId = new Guid("3a74e492-7c86-4008-8c1c-4c8d628b69ea"), DestinationStopId = new Guid("aa684895-576f-4496-8d41-9a911efae915"), StartTime = TimeSpan.Parse("11:01:00") },
            new SearchInputDto() { StartStopId = new Guid("46ca0f84-2599-450d-a9fe-21d437c8f1a0"), DestinationStopId = new Guid("00f56f3c-2057-4a75-ae35-2627fd090549"), StartTime = TimeSpan.Parse("15:05:00") },
            new SearchInputDto() { StartStopId = new Guid("5df78827-9fcd-48c8-923a-fab4cd67bb1e"), DestinationStopId = new Guid("9d930125-de2d-43e2-9c43-1be491b3e24e"), StartTime = TimeSpan.Parse("12:29:00") },
            new SearchInputDto() { StartStopId = new Guid("99020e13-bd99-467d-b57d-d684afb871d5"), DestinationStopId = new Guid("9e400fd7-bfc8-4713-ad53-061bba680003"), StartTime = TimeSpan.Parse("12:02:00") },
            new SearchInputDto() { StartStopId = new Guid("c7931592-bef8-44a1-b5ee-0a0dbe038bd4"), DestinationStopId = new Guid("b8d68506-4997-4bea-a53a-7b01fb312eef"), StartTime = TimeSpan.Parse("16:09:00") },
            new SearchInputDto() { StartStopId = new Guid("45f5f039-1d10-400b-b57e-6f8bc01c7a4b"), DestinationStopId = new Guid("363c2fab-717f-4043-b749-22515d72706a"), StartTime = TimeSpan.Parse("12:18:00") },
            new SearchInputDto() { StartStopId = new Guid("71664720-3e70-408b-8a09-c0e06e3af0c6"), DestinationStopId = new Guid("a72ba145-ce7d-4983-b8c8-7cf3ed90dc3d"), StartTime = TimeSpan.Parse("10:52:00") },
            new SearchInputDto() { StartStopId = new Guid("e5166aca-e506-48ad-a30a-8d4e0b79bf17"), DestinationStopId = new Guid("cd8cedba-9229-419f-8608-e3928ef934ae"), StartTime = TimeSpan.Parse("15:26:00") },
            new SearchInputDto() { StartStopId = new Guid("4ed45808-378e-4017-ac5f-d44e11bebfc9"), DestinationStopId = new Guid("8a231c68-28b0-433f-a7d0-04e96cca3d5e"), StartTime = TimeSpan.Parse("10:19:00") },
            new SearchInputDto() { StartStopId = new Guid("9bef9946-5297-48be-a9ba-48faeff76f12"), DestinationStopId = new Guid("7cb25de6-516d-4aa7-aa92-5f423362c33f"), StartTime = TimeSpan.Parse("13:33:00") },
            new SearchInputDto() { StartStopId = new Guid("fd6cfccb-5af6-49b8-bfb9-27e6d6f768c2"), DestinationStopId = new Guid("4ed45808-378e-4017-ac5f-d44e11bebfc9"), StartTime = TimeSpan.Parse("15:12:00") },
            new SearchInputDto() { StartStopId = new Guid("85987f0f-abf4-485f-972e-c4249f84a66f"), DestinationStopId = new Guid("ebd199a6-618f-49df-882c-397730e02073"), StartTime = TimeSpan.Parse("15:33:00") },
            new SearchInputDto() { StartStopId = new Guid("c93d3dcf-4637-4c4e-b44f-41a5e52dfe38"), DestinationStopId = new Guid("76f5ad65-27da-4a23-ae5a-f05b31869fb7"), StartTime = TimeSpan.Parse("12:08:00") },
            new SearchInputDto() { StartStopId = new Guid("133f37ca-7706-4b3f-98c7-1452774d8c79"), DestinationStopId = new Guid("1a6befd7-e74e-4185-9631-c78188ac8038"), StartTime = TimeSpan.Parse("14:26:00") },
            new SearchInputDto() { StartStopId = new Guid("d0fd62fc-1f7a-4cea-9bad-5744839937ec"), DestinationStopId = new Guid("4c0def33-f275-4bec-a534-57c093cd9c44"), StartTime = TimeSpan.Parse("16:34:00") },
            new SearchInputDto() { StartStopId = new Guid("63bbf45c-9ec2-4fdb-af27-159d54886152"), DestinationStopId = new Guid("d326671c-f4db-4d4d-b802-aaa5783be154"), StartTime = TimeSpan.Parse("11:01:00") },
            new SearchInputDto() { StartStopId = new Guid("9e1d3aa4-f1ce-4db0-adbd-2525367f2fbf"), DestinationStopId = new Guid("1143448c-559e-4f89-a469-5565f2e8a6a5"), StartTime = TimeSpan.Parse("11:05:00") },
            new SearchInputDto() { StartStopId = new Guid("9c6915e4-02f5-4be0-8d5a-2acdc0d557b5"), DestinationStopId = new Guid("1615e332-35be-42d9-8d81-cbab0874617d"), StartTime = TimeSpan.Parse("11:37:00") },
        };
    }
}
