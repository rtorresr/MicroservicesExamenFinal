using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Infrastructure.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var servicesUrl = configuration.GetSection("ServicesUrl");
            services.Configure<ServicesUrl>(servicesUrl);

            return services;
        }
    }
}
