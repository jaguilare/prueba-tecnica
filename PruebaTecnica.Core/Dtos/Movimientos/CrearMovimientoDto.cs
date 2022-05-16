using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Movimientos
{
    public class CrearMovimientoDto
    {
        public Movimiento Movimiento { get; set; }

        public static object Response(Movimiento movimiento)
        {
            if (movimiento is null) { return null; }

            var o = new
            {
                Movimiento = new
                {
                    MovimientoId = movimiento.MovimientosId
                }
            };
            return o;
        }
    }
}
