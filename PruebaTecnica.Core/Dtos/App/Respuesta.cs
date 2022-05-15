using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.App
{
    public class Respuesta
    {
        public Respuesta(string mensaje, ECodigoRespuesta codigo, object resultado)
        {
            Mensaje = mensaje;
            Codigo = codigo;
            Resultado = resultado;
        }

        public string Mensaje { get; set; }
        public ECodigoRespuesta Codigo { get; set; }
        public object Resultado { get; set; }

    }
}
