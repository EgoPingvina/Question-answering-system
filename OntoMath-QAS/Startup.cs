﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

using OntoMath_QAS.Middleware;
using OntoMath_QAS.Models.Settings;

using static OntoMath_QAS.AppConstants;

namespace OntoMath_QAS
{
    public sealed partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var endpoint = this.Configuration.GetSection(SparqlSettings.Key);

            services.Configure<SparqlSettings>(endpoint);


            services
                .AddMvc(option =>
                {
                    // Отключаем маршрутизацию конечных точек на основе endpoint-based logic из EndpointMiddleware
                    // и продолжаем использование маршрутизации на основе IRouter. 
                    option.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });


            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(API.Version, new OpenApiInfo
                    {
                        Version = API.Version,
                        Title   = API.Swagger.Title
                    });
                })
                .AddSwaggerGenNewtonsoftSupport();


            services.AddCors(options => options.AddPolicy(this.AllowAnyPolicyName, this.AllowAny));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandling>();

            app.UseHttpsRedirection()
               .UseRouting()
               .UseCors(this.AllowAnyPolicyName)
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               })
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.SwaggerEndpoint(API.Swagger.Endpoint, API.Swagger.Title);
               });
        }
    }
}