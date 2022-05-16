using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Clientes
{
    public class ConsultarClienteDto
    {
        public string Identificacion { get; set; }

        public static object Response(Cliente cliente)
        {
            var o = new
            {
                Persona = new
                {
                    PersonaId = cliente.Persona.PersonaId,
                    Nombre = cliente.Persona.Nombre,
                    Identificacion = cliente.Persona.Identificacion,
                    Genero = cliente.Persona.Genero,
                    Direccion = cliente.Persona.Direccion,
                    Edad = cliente.Persona.Edad
                },
                Cliente = new
                {
                    PersonaId = cliente.PersonaId,
                    ClienteId = cliente.ClienteId,
                    Estado = cliente.Estado,
                }
            };
            return o;
        }

    }
}
