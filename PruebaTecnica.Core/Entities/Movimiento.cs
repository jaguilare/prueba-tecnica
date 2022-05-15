using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaTecnica.Core
{
    public partial class Movimiento
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public int MovimientosId { get; set; }
        public int CuentaId { get; set; }

        public virtual Cuentum Cuenta { get; set; }
    }
}
