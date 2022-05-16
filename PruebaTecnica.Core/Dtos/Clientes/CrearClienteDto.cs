using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Clientes
{
    public class CrearClienteDto
    {

        public Persona Persona { get; set; }
        public Cliente Cliente { get; set; }

        public static object Response(Persona persona, Cliente cliente)
        {
            var o = new { 
                Persona = new {
                    PersonaId = persona.PersonaId
                },
                Cliente = new
                {
                    ClienteId = cliente.ClienteId
                },
            };
            return o;
        }


    }
}
