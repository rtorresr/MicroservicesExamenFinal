using Microservices.Demo.Reporte.API.Infrastructure.Agents.Product.Queries;
using Microservices.Demo.Reporte.API.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Infrastructure.Agents.Product
{
    using Microsoft.Extensions.Options;
    using Polly;
    using RestEase;
    using Steeltoe.Common.Discovery;
    using Steeltoe.Discovery;

    public class ProductClient : IProductClient
    {
        private readonly IProductClient client;
        public readonly ServicesUrl _servicesUrl;

        private static IAsyncPolicy retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(3));

        public ProductClient(IOptions<ServicesUrl> servicesUrl, IDiscoveryClient discoveryClient)
        {
            _servicesUrl = servicesUrl.Value;
            var handler = new DiscoveryHttpClientHandler(discoveryClient);
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var httpClient = new HttpClient(handler, false)
            {
                BaseAddress = new Uri(_servicesUrl.ProductApiUrl)
            };
            client = RestClient.For<IProductClient>(httpClient);
        }

        public Task<IEnumerable<ProductResult>> GetAll()
        {
            try
            {
                return retryPolicy.ExecuteAsync(async () => await client.GetAll());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
