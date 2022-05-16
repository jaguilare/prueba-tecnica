using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Request.Cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.Services
{
    public interface ICuentasService
    {
        public Task<Respuesta> Consultar(ConsultarCuentasDto dto);
        public Task<Respuesta> Consultar(ConsultarCuentaDto dto);
        public Task<Respuesta> Crear(CrearCuentaDto dto);
        public Task<Respuesta> Actualizar(ActualizarCuentaDto dto);
        public Task<Respuesta> Eliminar(EliminarCuentaDto dto);
    }
}
