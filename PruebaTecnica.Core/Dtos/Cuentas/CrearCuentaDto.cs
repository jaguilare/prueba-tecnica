using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Request.Cuentas
{
    public class CrearCuentaDto
    {
        public Cuentum Cuenta { get; set; }

        public static object Response(Cuentum cuenta)
        {
            var o = new
            {
                Cuenta = new
                {
                    CuentaId = cuenta.CuentaId
                }
            };
            return o;
        }
    }
}
