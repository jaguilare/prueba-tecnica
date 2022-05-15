using PruebaTecnica.Core.Dtos.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.IRepositories
{
    public interface IClientesRepository
    {

        public Task<IList<Cliente>> Consultar(string identificacion);
        public Cliente Crear(Cliente dto);
        public Cliente Actualizar(Cliente dto);
        public Cliente Eliminar(Cliente dto);
        public Task<int> GuardarCambiosOk();

    }
}
