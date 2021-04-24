using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace OntoMath_QAS
{
    public partial class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                   .UseServiceProviderFactory(new DryIocServiceProviderFactory(Container))
                   .UseSerilog((context, services, options) =>
                   {
                       options
                           .ReadFrom.Configuration(context.Configuration)
                           .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Debug)
                           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                           .MinimumLevel.Override("System", LogEventLevel.Warning)
                           .Enrich.FromLogContext()
#if DEBUG
                           .WriteTo.Console(LogEventLevel.Information)
                           .WriteTo.Debug(LogEventLevel.Information)
#endif
#if !SEQ
                           .WriteTo.Seq(
                               "http://localhost:5341",
                               LogEventLevel.Information)
#endif
                           ;
                   })
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
                   })
                   .ConfigureContainer<Container>((context, container) =>
                   {
                       Program.Register(container);
                   });

        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .Run();
    }
}