using Chilicki.Ptsa.Domain.Search.Exceptions;
using Chilicki.Ptsa.Search.Configurations.Startup;
using System;
using System.Threading.Tasks;

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
                if (ex.GetType() != typeof(DijkstraNoFastestPathExistsException))
                    Console.WriteLine("There was unhandled exception");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
