using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Movimientos
{
    public class ReportarDto
    {

        public string Identificacion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }



        public class ReporteRespose
        {
            public int PersonaId { get; set; }
            public string Identificacion { get; set; }
            public string Nombre { get; set; }


            public string NumeroCuenta { get; set; }
            public string TipoCuenta { get; set; }
            public decimal SaldoInicial { get; set; }
            public bool Estado { get; set; }


            public string TipoMovimiento { get; set; }
            public decimal Valor { get; set; }
            public decimal Saldo { get; set; }
            public DateTime Fecha { get; set; }


        }


    }
}
