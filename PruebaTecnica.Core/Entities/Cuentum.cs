using System;
using System.Collections.Generic;

#nullable disable

namespace PruebaTecnica.Core
{
    public partial class Cuentum
    {
        public Cuentum()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int CuentaId { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
