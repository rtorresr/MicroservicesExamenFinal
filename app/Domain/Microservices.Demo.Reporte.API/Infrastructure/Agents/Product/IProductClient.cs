using Microservices.Demo.Reporte.API.Infrastructure.Agents.Product.Queries;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Infrastructure.Agents.Product
{
    public interface IProductClient
    {
        [Get]
        Task<IEnumerable<ProductResult>> GetAll();
    }
}
