using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FleetMgmt.Web
{
    public class Program
    {
        // public static void Main(string[] args)
        // {
        //     BuildWebHost(args).Run();
        // }
        //
        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>()
        //         .Build();
        
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();
        
        // EF Core uses this method at design time to access the DbContext
            public static IHostBuilder CreateHostBuilder(string[] args)
                => Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(
                        webBuilder => webBuilder.UseStartup<Startup>());
    }
}
