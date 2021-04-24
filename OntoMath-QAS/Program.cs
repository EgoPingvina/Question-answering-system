using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OntoMath_QAS
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context, options) =>
                   {
                       options
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddEnvironmentVariables();
                   })
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>()
                                 .UseIIS();
                   });

        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .Run();
    }
}