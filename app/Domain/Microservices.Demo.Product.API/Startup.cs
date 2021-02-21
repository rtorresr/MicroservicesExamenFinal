using MediatR;
using Microservices.Demo.Product.API.Application;
using Microservices.Demo.Product.API.Domain;
using Microservices.Demo.Product.API.Infrastructure.Configuration;
using Microservices.Demo.Product.API.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Steeltoe.Discovery.Client;
using System.Reflection;

namespace Microservices.Demo.Product.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Demo.Product.API", Version = "v1" });
            });

            services.AddControllers()
            .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                 options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDiscoveryClient();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Demo.Product.API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
