using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Nancy.Hosting.Self;

namespace HelloWorld
{
    public class Program
    {
        //static void Main(string[] args)
        //{
        //    var url = "http://localhost:1234";

        //    /************** Nancy Self Hosting ***************/
        //    //using (var host = new NancyHost(new Uri(url)))
        //    //{
        //    //    Console.WriteLine("Starting on port:- " + url);

        //    //    host.Start();  // start hosting

        //    //    Console.WriteLine("started!");

        //    //    Console.ReadKey();
        //    //}

        //    using (WebApp.Start<Startup>(url))
        //    {
        //        Console.WriteLine("Running on {0}", url);
        //        Console.WriteLine("Press enter to exit");
        //        Console.ReadLine();
        //    }
        //}

        public static void Main(string[] args)
        {
            string uri = "http://10.109.210.186:1234/";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started");
                Console.ReadKey();
                Console.WriteLine("Stopping");
            }
        }
    }
}
