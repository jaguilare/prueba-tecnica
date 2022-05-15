using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Core;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Clientes;
using PruebaTecnica.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace PruebaTecica.Api.Controllers
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
        public async Task<ActionResult<Respuesta>> CrearCliente(CrearClienteDto dto)
        {
            Respuesta respuesta = await _clientesService.Crear(dto);
            return respuesta;
        }


    }
}
