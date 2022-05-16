using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Clientes;
using PruebaTecnica.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace PruebaTecnica.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService personasService)
        {
            _clientesService = personasService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Crear(CrearClienteDto dto)
        {
            Respuesta respuesta = await _clientesService.Crear(dto);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(ActualizarClienteDto dto)
        {
            Respuesta respuesta = await _clientesService.Actualizar(dto);
            return respuesta;
        }

        [HttpDelete]
        public async Task<ActionResult<Respuesta>> Eliminar(EliminarClienteDto dto)
        {
            Respuesta respuesta = await _clientesService.Eliminar(dto);
            return respuesta;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> Consultar([FromQuery] ConsultarClienteDto dto)
        {
            Respuesta respuesta = await _clientesService.Consultar(dto);
            return respuesta;
        }


    }
}
