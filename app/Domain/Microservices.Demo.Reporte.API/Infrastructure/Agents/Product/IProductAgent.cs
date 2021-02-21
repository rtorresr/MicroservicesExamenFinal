using Microservices.Demo.Reporte.API.Infrastructure.Agents.Product.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Infrastructure.Agents.Product
{
    public interface IProductAgent
    {
        Task<IEnumerable<ProductResult>> GetAll();
    }
}
