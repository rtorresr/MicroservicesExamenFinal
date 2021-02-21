using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microservices.Demo.Pricing.API.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;
using MediatR;
using Microservices.Demo.Pricing.API.Application;
using Microservices.Demo.Pricing.API.Domain;
using Microservices.Demo.Pricing.API.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Microservices.Demo.Pricing.API.CQRS.Commands.Infrastructure.Dtos.Converters;
using GlobalExceptionHandler.WebApi;
using Microservices.Demo.Pricing.API.Infrastructure.Exception;

namespace Microservices.Demo.Pricing.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);
            services.AddConfigurations(Configuration);            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplicationServices();
            services.AddDomainServices();
            services.AddDataServices(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Demo.Pricing.API", Version = "v1" });
            });
            services.AddControllers()
            .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 options.SerializerSettings.Converters.Add(new QuestionAnswerDtoConverter());
             });


            services.AddPricingDemoInitializer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionHandler(cfg => cfg.MapExceptions());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDbInitializer();

            app.UseDiscoveryClient();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Demo.Pricing.API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
