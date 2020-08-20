using Chilicki.Ptsa.Domain.Search.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.InputSearches
{
    public static class LahtiInputSearches
    {
        public static IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("6461e130-d6ad-4d1c-b4f7-859e27fe823c"), DestinationStopId = new Guid("b1eeff15-43f2-468c-a009-d32c9db9dcbb"), StartTime = TimeSpan.Parse("15:21:00") },
            new SearchInputDto() { StartStopId = new Guid("85a35c50-d175-4c6e-aa27-54dcb38b00c0"), DestinationStopId = new Guid("6821ea25-877e-4e1a-8e38-07c783f79a25"), StartTime = TimeSpan.Parse("13:25:00") },
            new SearchInputDto() { StartStopId = new Guid("24f1fa82-e5aa-48b6-a002-ff1a70d972a1"), DestinationStopId = new Guid("5fafb1d9-aecc-4470-8635-ab23ba62f32b"), StartTime = TimeSpan.Parse("11:42:00") },
            new SearchInputDto() { StartStopId = new Guid("6de0ed22-36af-4e00-a450-d005b13d426c"), DestinationStopId = new Guid("a1cbdb3b-2cac-4b58-89d1-0a8e5eea2c65"), StartTime = TimeSpan.Parse("12:28:00") },
            new SearchInputDto() { StartStopId = new Guid("f8b056e1-e720-4751-93a8-21635e183198"), DestinationStopId = new Guid("943b6215-a242-48cb-a7cb-e6b1294556ca"), StartTime = TimeSpan.Parse("12:41:00") },
            new SearchInputDto() { StartStopId = new Guid("cc8b0d6f-c2fe-4d38-8add-9cd361422b51"), DestinationStopId = new Guid("fd5ae881-f6c5-4df6-b930-5ef5e7439cba"), StartTime = TimeSpan.Parse("10:58:00") },
            new SearchInputDto() { StartStopId = new Guid("1ec9a896-087f-4669-8f79-0ae99e6fbcd6"), DestinationStopId = new Guid("53f6ef98-222b-491f-8d24-1f4d8fb54e16"), StartTime = TimeSpan.Parse("10:13:00") },
            new SearchInputDto() { StartStopId = new Guid("4b740a11-6985-416d-bf91-68673946880c"), DestinationStopId = new Guid("07577c96-a8d9-403e-85f9-3a95bd3fbc35"), StartTime = TimeSpan.Parse("14:18:00") },
            new SearchInputDto() { StartStopId = new Guid("adebde90-1cfa-4a16-b5c6-7bff15e7d342"), DestinationStopId = new Guid("e8c30500-549f-4522-852d-d87ff31a2d40"), StartTime = TimeSpan.Parse("11:09:00") },
            new SearchInputDto() { StartStopId = new Guid("5fb44569-6dec-476d-b19a-9a3231a3bfde"), DestinationStopId = new Guid("cb14098a-c1ee-4a1c-a9ac-06c28036759b"), StartTime = TimeSpan.Parse("10:18:00") },
            new SearchInputDto() { StartStopId = new Guid("4b8d4e1f-da4b-4252-bf87-2aff1666d48b"), DestinationStopId = new Guid("08598d0f-1e08-4283-8b0c-38929d28a158"), StartTime = TimeSpan.Parse("13:09:00") },
            new SearchInputDto() { StartStopId = new Guid("f37e5330-db1c-4d47-bf65-a594f7f82c8d"), DestinationStopId = new Guid("5eea0c9a-8e7a-46e0-a42e-fcd3f5026059"), StartTime = TimeSpan.Parse("10:27:00") },
            new SearchInputDto() { StartStopId = new Guid("eaf83384-753d-4a86-9a56-b262749e1404"), DestinationStopId = new Guid("d16e2444-b002-4d58-9bd9-17043a970d95"), StartTime = TimeSpan.Parse("12:03:00") },
            new SearchInputDto() { StartStopId = new Guid("2ca6b716-1720-4e2c-9c86-0d10f522fd7d"), DestinationStopId = new Guid("124e8f21-6fb4-427c-a659-623c32525a34"), StartTime = TimeSpan.Parse("11:43:00") },
            new SearchInputDto() { StartStopId = new Guid("54181b95-27a4-4ec2-bf9c-6a112bb23ff7"), DestinationStopId = new Guid("0d2b5b44-48fd-481c-a510-03b8a2228d30"), StartTime = TimeSpan.Parse("10:13:00") },
            new SearchInputDto() { StartStopId = new Guid("0af830b3-ccfd-47c5-a24d-e31bee013dd1"), DestinationStopId = new Guid("21e901a2-68ea-4b12-9141-26ff646c55c7"), StartTime = TimeSpan.Parse("10:02:00") },
            new SearchInputDto() { StartStopId = new Guid("2307aa82-4f29-4768-b6d5-21584b041c9f"), DestinationStopId = new Guid("87521f13-c227-4439-8163-afb61651349c"), StartTime = TimeSpan.Parse("10:59:00") },
            new SearchInputDto() { StartStopId = new Guid("aae2c3e1-1211-4ea6-9224-4b357ebe8526"), DestinationStopId = new Guid("fd5ae881-f6c5-4df6-b930-5ef5e7439cba"), StartTime = TimeSpan.Parse("10:05:00") },
            new SearchInputDto() { StartStopId = new Guid("f8597a2e-6605-4d4b-90ba-6be05b5c8648"), DestinationStopId = new Guid("f61cfda9-e97a-483b-80ee-159d3c75e7eb"), StartTime = TimeSpan.Parse("14:02:00") },
            new SearchInputDto() { StartStopId = new Guid("43a8de69-137a-481c-8ec3-237825d3b698"), DestinationStopId = new Guid("e8c30500-549f-4522-852d-d87ff31a2d40"), StartTime = TimeSpan.Parse("11:07:00") },
            new SearchInputDto() { StartStopId = new Guid("3d539612-3ea7-4cac-9a53-50a2029114e2"), DestinationStopId = new Guid("9fe4ac29-580a-4c87-bc88-3b618250521e"), StartTime = TimeSpan.Parse("14:42:00") },
            new SearchInputDto() { StartStopId = new Guid("cc8b0d6f-c2fe-4d38-8add-9cd361422b51"), DestinationStopId = new Guid("7fad5133-80bd-474a-b536-9cd139580b55"), StartTime = TimeSpan.Parse("15:38:00") },
            new SearchInputDto() { StartStopId = new Guid("943b6215-a242-48cb-a7cb-e6b1294556ca"), DestinationStopId = new Guid("18918f0c-acd8-41d4-9175-7504a018381b"), StartTime = TimeSpan.Parse("10:27:00") },
            new SearchInputDto() { StartStopId = new Guid("5037db0e-a7c7-48ef-873a-5047860adfe5"), DestinationStopId = new Guid("b2c7a298-da0b-4fd8-b189-922f8c782c9f"), StartTime = TimeSpan.Parse("11:28:00") },
            new SearchInputDto() { StartStopId = new Guid("b24228bb-b1b7-43fc-bfc7-636d51c24d80"), DestinationStopId = new Guid("24f1fa82-e5aa-48b6-a002-ff1a70d972a1"), StartTime = TimeSpan.Parse("15:04:00") },
        };
    }
}
