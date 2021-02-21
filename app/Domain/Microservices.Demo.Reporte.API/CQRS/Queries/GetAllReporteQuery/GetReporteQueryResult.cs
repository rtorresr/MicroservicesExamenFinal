using Microservices.Demo.Reporte.API.CQRS.Queries.Infrastructure.Dtos.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.CQRS.Queries.GetAllReporteQuery
{
    public class GetReporteQueryResult
    {
        public GetReporteQueryResult()
        {
            Reporte = new List<ReporteDto>();
        }
        public IEnumerable<ReporteDto> Reporte { get; set; }
    }
}
