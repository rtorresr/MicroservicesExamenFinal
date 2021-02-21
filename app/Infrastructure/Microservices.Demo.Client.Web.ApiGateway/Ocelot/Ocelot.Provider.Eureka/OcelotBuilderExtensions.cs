using Ocelot.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.ServiceDiscovery;
using Steeltoe.Discovery.Client;
using System.Linq;

namespace Ocelot.Provider.Eureka
{
    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddEureka(this IOcelotBuilder builder)
        {
            builder.Services.AddDiscoveryClient(builder.Configuration);
            builder.Services.AddSingleton<ServiceDiscoveryFinderDelegate>(EurekaProviderFactory.Get);
            builder.Services.AddSingleton<OcelotMiddlewareConfigurationDelegate>(EurekaMiddlewareConfigurationProvider.Get);
            return builder;
        }
    }
}
