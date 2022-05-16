using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Movimientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Interfaces.Services
{
    public interface IMovimientosService
    {
        public Task<Respuesta> Reportar(ReportarDto dto);
        //public Task<Respuesta> Consultar(int movimientoId, int cuentaId);
        //public Task<Respuesta> Consultar(ConsultarMovimientosDto dto);
        public Task<Respuesta> Crear(CrearMovimientoDto dto);
        public Task<Respuesta> Actualizar(ActualizarMovimientoDto dto);
        public Task<Respuesta> Eliminar(EliminarMovimientoDto dto);

    }
}
