using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Request.Cuentas
{
    public class ConsultarCuentasDto
    {
        public int ClienteId { get; set; }

        public static object Response(IList<Cuentum> cuenta)
        {
            List<object> l = new List<object>();

            foreach (var c in cuenta)
            {
                l.Add(new
                {
                    Cuenta = new
                    {
                        CuentaId = c.CuentaId,
                        ClienteId = c.ClienteId,
                        NumeroCuenta = c.NumeroCuenta,
                        TipoCuenta = c.TipoCuenta,
                        SaldoInicial = c.SaldoInicial,
                        Estado = c.Estado
                    }
                });
            }
            return l;
        }
    }
}
