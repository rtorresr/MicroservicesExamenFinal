using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace Microservices.Demo.Client.Web.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((hostingContext, config) =>
                //{
                //    config
                //        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                //        .AddJsonFile("appsettings.json", true, true)
                //        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                //            true)
                //        .AddJsonFile("ocelot.json", false, false)
                //        .AddEnvironmentVariables();
                //})
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
                {

                    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
                    configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
