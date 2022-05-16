using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Clientes;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.Services
{
    public interface IClientesService
    {
        public Task<Respuesta> Consultar(ConsultarClienteDto dto);
        public Task<Respuesta> Crear(CrearClienteDto dto);
        public Task<Respuesta> Actualizar(ActualizarClienteDto dto);
        public Task<Respuesta> Eliminar(EliminarClienteDto dto);
    }
}
