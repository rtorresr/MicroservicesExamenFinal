using Microservices.Demo.Reporte.API.Infrastructure.Agents.Product.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Infrastructure.Agents.Product
{
    public class ProductAgent : IProductAgent
    {
        private readonly IProductClient productClient;

        public ProductAgent(IProductClient productClient)
        {
            this.productClient = productClient;
        }

        public async Task<IEnumerable<ProductResult>> GetAll()
        {
            try
            {
                return await productClient.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
