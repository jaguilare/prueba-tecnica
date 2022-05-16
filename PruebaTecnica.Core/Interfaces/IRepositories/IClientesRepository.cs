using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.IRepositories
{
    public interface IClientesRepository
    {

        public Task<Cliente> Consultar(string identificacion);
        public Cliente Crear(Cliente dto);
        public Cliente Actualizar(Cliente dto);
        public void Eliminar(int personaId);
        public Task<int> Guardar();

    }
}
