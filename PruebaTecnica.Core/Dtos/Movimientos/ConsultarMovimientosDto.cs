using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Movimientos
{
    public class ConsultarMovimientosDto
    {
        public int CuentaId { get; set; }

        public static object Response(IList<Movimiento> mvts)
        {
            if (mvts is null) { return null; }

            List<object> l = new List<object>();

            foreach (var m in mvts)
            {
                l.Add(new
                {
                    Movimiento = new
                    {
                        MovimientoId = m.MovimientosId,
                        CuentaId = m.CuentaId,
                        TipoMovimiento = m.TipoMovimiento,
                        Fecha = m.Fecha,
                        Valor = m.Valor,
                        Saldo = m.Saldo
                    }
                });
            }
            return l;
        }
    }
}
