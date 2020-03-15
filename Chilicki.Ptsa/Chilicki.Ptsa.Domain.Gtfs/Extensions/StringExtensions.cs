namespace Chilicki.Ptsa.Domain.Gtfs.Extensions
{
    public static class StringExtensions
    {
        public static string Clean(this string str)
        {
            return str.Replace("\"", string.Empty);
        }
    }
}
