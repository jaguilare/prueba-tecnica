using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.IRepositories
{
    public interface ICuentasRepository
    {

        public Task<Cuentum> Consultar(int cuentaId, string numeroCuenta, int clienteId);
        public Task<Cuentum> Consultar(string numeroCuenta, int clienteId);
        public Task<IList<Cuentum>> Consultar(int clienteId);
        public Cuentum Crear(Cuentum dto);
        public Cuentum Actualizar(Cuentum dto);
        public void Eliminar(string numeroCuenta, int clienteId);
        public Task<int> Guardar();

    }
}
