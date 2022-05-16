using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Request.Cuentas
{
    public class EliminarCuentaDto
    {
        public int CuentaId { get; set; }
        public int ClienteId { get; set; }
        public string NumeroCuenta { get; set; }

    }
}
