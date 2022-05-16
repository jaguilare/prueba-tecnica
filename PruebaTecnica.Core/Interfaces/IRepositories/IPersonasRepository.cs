using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.IRepositories
{
    public interface IPersonasRepository
    {
        public Task<Persona> Consultar(string identificacion);
        public Task<Persona> ConsultarPersonaCliente(string identificacion);

        public Persona Crear(Persona dto);
        public Persona Actualizar(Persona dto);
        public void Eliminar(int personaId);
        public Task<int> Guardar();

    }
}
