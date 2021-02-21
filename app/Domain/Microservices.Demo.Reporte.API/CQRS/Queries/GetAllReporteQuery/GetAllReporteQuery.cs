using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.CQRS.Queries.GetAllReporteQuery
{
    public class GetAllReporteQuery : IRequest<GetReporteQueryResult>
    {
    }
}
