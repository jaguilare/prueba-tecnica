using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Core.Dtos.Clientes
{
    public class ActualizarClienteDto
    {
        public Persona Persona { get; set; }
        public Cliente Cliente { get; set; }
    }
}
