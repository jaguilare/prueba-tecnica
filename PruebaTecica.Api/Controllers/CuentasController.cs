using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Request.Cuentas;
using PruebaTecnica.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace PruebaTecnica.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {

        private readonly ICuentasService _cuentasService;

        public CuentasController(ICuentasService cuentasService)
        {
            _cuentasService = cuentasService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Crear(CrearCuentaDto dto)
        {
            Respuesta respuesta = await _cuentasService.Crear(dto);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(ActualizarCuentaDto dto)
        {
            Respuesta respuesta = await _cuentasService.Actualizar(dto);
            return respuesta;
        }

        [HttpDelete]
        public async Task<ActionResult<Respuesta>> Eliminar(EliminarCuentaDto dto)
        {
            Respuesta respuesta = await _cuentasService.Eliminar(dto);
            return respuesta;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> Consultar([FromQuery] ConsultarCuentasDto dto)
        {
            Respuesta respuesta = await _cuentasService.Consultar(dto);
            return respuesta;
        }

    }
}
