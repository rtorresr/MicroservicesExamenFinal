using MediatR;
using Microservices.Demo.Reporte.API.CQRS.Queries.GetAllReporteQuery;
using Microservices.Demo.Reporte.API.CQRS.Queries.Infrastructure.Dtos.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.Application
{
    public class ReporteApplicationService
    {
        private readonly IMediator _mediator;
        public ReporteApplicationService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<GetReporteQueryResult> GetAllAsync()
        {
            return await _mediator.Send(new GetAllReporteQuery());
        }
    }
}
