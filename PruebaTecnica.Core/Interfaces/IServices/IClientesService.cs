using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Clientes;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.Services
{
    public interface IClientesService
    {
        public Task<Respuesta> Consultar(string identificacion);
        public Task<Respuesta> Crear(CrearClienteDto dto);
        public Task<Respuesta> Actualizar(Persona dto);
        public Task<Respuesta> Eliminar(Persona dto);
    }
}
