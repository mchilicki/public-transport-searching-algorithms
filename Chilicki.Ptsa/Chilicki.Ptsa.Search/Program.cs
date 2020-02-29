using Chilicki.Ptsa.Search.Configurations.Startup;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chilicki.Ptsa.Search
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            new StartupService().Run();
            Console.ReadKey();
        }
    }
}
