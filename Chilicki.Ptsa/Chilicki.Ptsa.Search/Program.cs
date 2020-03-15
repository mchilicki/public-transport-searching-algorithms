using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using Chilicki.Ptsa.Search.Configurations.Startup;
using System;

namespace Chilicki.Ptsa.Search
{
    public static class Program
    {
        public static void Main()
        {
            try
            {
                var task = new StartupService().Run();
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was unhandled exception");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
