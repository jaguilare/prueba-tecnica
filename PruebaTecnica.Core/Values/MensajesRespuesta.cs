using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Values
{
    public static class MensajesRespuesta
    {

        public const string OK = "Proceso exitoso";
        public const string ERROR = "Error, proceso no ejecutado";
        public const string CLIENTE_YA_EXISTE = "El cliente ya se encuentra registrado en la base de datos";
        public const string CLIENTE_NO_ENCONTRADO = "El cliente no se encontró en la base de datos";

        public const string CUENTA_YA_EXISTE = "La cuenta ya se encuentra registrada en la base de datos";
        public const string CUENTA_NO_ENCONTRADA = "La cuenta no se encontró en la base de datos";

        public const string MOVIMIENTO_NO_ENCONTRADO = "El movimiento no se encontró en la base de datos";
        public const string MOVIMIENTO_SALDO_NO_DISPONIBLE = "Saldo no disponible";
        public const string MOVIMIENTO_CUPO_DIARIO_EXCEDIDO = "Cupo diario excedido";

        public const string REPORTE_SIN_DATOS = "No se encontraron datos que reportar";
        public const string REPORTE_ERROR = "Ocurrió un error al generar el reporte";

    }
}
