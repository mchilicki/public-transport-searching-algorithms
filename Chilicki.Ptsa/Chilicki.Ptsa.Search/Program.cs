using Chilicki.Ptsa.Search.Configurations.Startup;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Task task = new StartupService().Run();
             task.Wait();
        }
    }
}
