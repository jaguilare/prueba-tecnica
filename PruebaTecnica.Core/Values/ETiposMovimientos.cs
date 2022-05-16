using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Values
{
    public enum ETiposMovimientos
    {
        CREDITO = 1,
        DEBITO = -1
    }

    public static class TiposMovimientos
    {
        public const string CREDITO = "CREDITO";
        public const string DEBITO = "DEBITO";
    }

}
