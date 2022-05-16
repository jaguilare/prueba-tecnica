using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Movimientos;
using PruebaTecnica.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace PruebaTecnica.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {

        private readonly IMovimientosService _movimientosService;

        public MovimientosController(IMovimientosService movimientosService)
        {
            _movimientosService = movimientosService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Crear(CrearMovimientoDto dto)
        {
            Respuesta respuesta = await _movimientosService.Crear(dto);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(ActualizarMovimientoDto dto)
        {
            Respuesta respuesta = await _movimientosService.Actualizar(dto);
            return respuesta;
        }

        [HttpDelete]
        public async Task<ActionResult<Respuesta>> Eliminar(EliminarMovimientoDto dto)
        {
            Respuesta respuesta = await _movimientosService.Eliminar(dto);
            return respuesta;
        }

        [HttpGet("reportar")]
        public async Task<ActionResult<Respuesta>> Reportar([FromQuery] ReportarDto dto)
        {
            Respuesta respuesta = await _movimientosService.Reportar(dto);
            return respuesta;
        }

    }
}
