using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.IRepositories
{
    public interface IPersonasRepository
    {
        public Task<Persona> Consultar(string identificacion);
        public Persona Crear(Persona dto);
        public Persona Actualizar(Persona dto);
        public Persona Eliminar(Persona dto);
        public Task<int> GuardarCambiosOk();

    }
}
