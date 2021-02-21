using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Demo.Reporte.API.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Reporte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteApplicationService _reporteApplicationService;
        public ReporteController(ReporteApplicationService reporteApplicationService)
        {
            _reporteApplicationService = reporteApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
			return new JsonResult(await _reporteApplicationService.GetAllAsync());
        }
    }
}