using MediatR;
using Microservices.Demo.Reporte.API.CQRS.Queries.Infrastructure.Dtos.Reporte;
using Microservices.Demo.Reporte.API.Infrastructure.Agents.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.CQRS.Queries.GetAllReporteQuery
{
    public class GetAllReporteHandler : IRequestHandler<GetAllReporteQuery,GetReporteQueryResult>
    {
        private readonly IProductAgent _productAgent;

        public GetAllReporteHandler(IProductAgent productAgent)
        {
            _productAgent = productAgent;
        }

        public async Task<GetReporteQueryResult> Handle(GetAllReporteQuery request, CancellationToken cancellationToken)
        {
            var productos = await _productAgent.GetAll();

            var result = new GetReporteQueryResult();

            foreach (var item in productos)
            {
               result.Reporte.ToList().Add(new ReporteDto()
               {
                   NombreProducto = item.Name,
                   Descripcion = item.Description,
                   CodigoProducto = item.Code,
                   NombreFoto = item.Image
               });
            }

            return result;
        }
    }
}
