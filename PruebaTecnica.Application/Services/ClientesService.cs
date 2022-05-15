using PruebaTecnica.Core;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Clientes;
using PruebaTecnica.Core.Dtos.Values;
using PruebaTecnica.Core.Interfaces.IRepositories;
using PruebaTecnica.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PruebaTecnica.Application.Services
{
    public class ClientesService : IClientesService
    {

        private readonly IPersonasRepository _personaRepository;
        private readonly IClientesRepository _clientesRepository;


        public ClientesService(
            IPersonasRepository personaRepository,
            IClientesRepository clienesRepository
            )
        {
            _personaRepository = personaRepository;
            _clientesRepository = clienesRepository;
        }

        public async Task<Respuesta> Consultar(string identificacion)
        {
            Persona persona = null;
            string mensajeRespuesta = MensajesRespuesta.OK;
            ECodigoRespuesta codigoRespuesta = ECodigoRespuesta.OK;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    persona = await _personaRepository.Consultar(identificacion);

                    if (persona is null)
                    {
                        codigoRespuesta = ECodigoRespuesta.ERROR;
                        mensajeRespuesta = MensajesRespuesta.ERROR;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    codigoRespuesta = ECodigoRespuesta.ERROR;
                    mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(mensajeRespuesta, codigoRespuesta, persona);

        }

        public async Task<Respuesta> Crear(CrearClienteDto dto)
        {
            Persona persona = null;
            Cliente cliente = null;
            string mensajeRespuesta = MensajesRespuesta.OK;
            ECodigoRespuesta codigoRespuesta = ECodigoRespuesta.OK;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    //persona = await _personaRepository.Consultar(dto.Persona.Identificacion);
                    IList<Cliente> clientes = await _clientesRepository.Consultar(dto.Persona.Identificacion);
                    cliente = clientes.FirstOrDefault();

                    if (cliente is null)
                    {
                        persona = _personaRepository.Crear(dto.Persona);
                        int personasAfectadas = await _personaRepository.GuardarCambiosOk();

                        dto.Cliente.PersonaId = persona.PersonaId;
                        cliente = _clientesRepository.Crear(dto.Cliente);

                        int clientesAfectados = await _clientesRepository.GuardarCambiosOk();

                        scope.Complete();
                    }
                    else
                    {
                        cliente = null;
                        codigoRespuesta = ECodigoRespuesta.ERROR;
                        mensajeRespuesta = MensajesRespuesta.ERROR_PERSONA_YA_EXISTE;
                    }
                }
                catch (Exception exc)
                {
                    persona = null;
                    cliente = null;
                    Console.WriteLine($"Consultar() => {exc}");
                    codigoRespuesta = ECodigoRespuesta.ERROR;
                    mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(mensajeRespuesta, codigoRespuesta, new { persona, cliente });
        }

        public Task<Respuesta> Actualizar(Persona dto)
        {
            throw new NotImplementedException();
        }



        public Task<Respuesta> Eliminar(Persona dto)
        {
            throw new NotImplementedException();
        }

    }
}
