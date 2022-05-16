using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PruebaTecnica.Core.Dtos.Movimientos.ReportarDto;

namespace PruebaTecnica.Core.Interfaces.IRepositories
{
    public interface IMovimientosRepository
    {
        public Task<IList<ReporteRespose>> Reportar(string identificacion, DateTime fechaInicial, DateTime fechaFinal);
        public Task<Movimiento> Consultar(int movimientoId, int cuentaId);
        public Task<IList<Movimiento>> Consultar(int cuentaId, DateTime fecha, string tipoMovimiento);
        public Task<IList<Movimiento>> Consultar(int cuentaId, DateTime fecha);
        public Movimiento Crear(Movimiento dto);
        public Movimiento Actualizar(Movimiento dto);
        public void Eliminar(int movimientoId, int cuentaId);
        public Task<int> Guardar();

    }
}
