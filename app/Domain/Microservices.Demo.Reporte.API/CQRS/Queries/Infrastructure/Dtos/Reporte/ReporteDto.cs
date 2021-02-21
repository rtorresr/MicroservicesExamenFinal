using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Reporte.API.CQRS.Queries.Infrastructure.Dtos.Reporte
{
    public class ReporteDto
    {
        public string Numero { get; set; }
        public string NombreProducto { get; set; }
        public string NombreFoto { get; set; }
        public string Descripcion { get; set; }
        public string CodigoProducto { get; set; }
        public string AgentLogin { get; set; }
        public string PolicyStatusId { get; set; }
    }
}
