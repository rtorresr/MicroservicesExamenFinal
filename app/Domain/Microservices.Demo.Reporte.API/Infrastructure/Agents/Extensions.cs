using Microservices.Demo.Reporte.API.Infrastructure.Agents.Product;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Infrastructure.Agents
{
    public static class Extensions
    {
        public static IServiceCollection AddRestClients(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IProductClient), typeof(ProductClient));
            services.AddSingleton(typeof(IProductAgent), typeof(ProductAgent));
            return services;
        }
    }
}
