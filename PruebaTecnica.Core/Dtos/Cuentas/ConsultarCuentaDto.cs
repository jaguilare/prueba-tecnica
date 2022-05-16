using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Request.Cuentas
{
    public class ConsultarCuentaDto
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public int ClienteId { get; set; }

    }
}
