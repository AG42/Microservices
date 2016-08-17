using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var microService = new MicroService4Net.MicroService(1235);
            microService.Run(args);
        }
    }
}
    