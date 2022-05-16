using PruebaTecnica.Core;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Clientes;
using PruebaTecnica.Core.Values;
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

        private Persona _persona;
        private Cliente _cliente;
        private string _mensajeRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        private readonly IPersonasRepository _personasRepository;
        private readonly IClientesRepository _clientesRepository;


        public ClientesService(
            IPersonasRepository personaRepository,
            IClientesRepository clienesRepository
            )
        {
            _mensajeRespuesta = MensajesRespuesta.OK;
            _codigoRespuesta = ECodigoRespuesta.OK;
            _personasRepository = personaRepository;
            _clientesRepository = clienesRepository;
        }

        public async Task<Respuesta> Consultar(ConsultarClienteDto dto)
        {
            try
            {
                _cliente = await _clientesRepository.Consultar(dto.Identificacion);

                if (_cliente is null)
                {
                    _mensajeRespuesta = MensajesRespuesta.CLIENTE_NO_ENCONTRADO;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Consultar() => {exc}");
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _mensajeRespuesta = MensajesRespuesta.ERROR;
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, ConsultarClienteDto.Response(_cliente));
        }

        public async Task<Respuesta> Crear(CrearClienteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _cliente = await _clientesRepository.Consultar(dto.Persona.Identificacion);

                    if (_cliente is null)
                    {
                        _persona = _personasRepository.Crear(dto.Persona);
                        int personasAfectadas = await _personasRepository.Guardar();

                        dto.Cliente.PersonaId = _persona.PersonaId;
                        _cliente = _clientesRepository.Crear(dto.Cliente);
                        int clientesAfectados = await _clientesRepository.Guardar();

                        scope.Complete();
                    }
                    else
                    {
                        _cliente = null;
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.CLIENTE_YA_EXISTE;
                    }
                }
                catch (Exception exc)
                {
                    _cliente = null;
                    Console.WriteLine($"Crear() => {exc}");
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, CrearClienteDto.Response(_persona, _cliente));
        }

        public async Task<Respuesta> Actualizar(ActualizarClienteDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _cliente = await _clientesRepository.Consultar(dto.Persona.Identificacion);

                    if (_cliente is not null)
                    {
                        _clientesRepository.Actualizar(dto.Cliente);
                        int clientesAfectados = await _clientesRepository.Guardar();

                        _personasRepository.Actualizar(dto.Persona);
                        int personasAfectadas = await _personasRepository.Guardar();

                        scope.Complete();
                    }
                    else
                    {
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.CLIENTE_NO_ENCONTRADO;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Actualizar() => {exc}");
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, null);
        }



        public async Task<Respuesta> Eliminar(EliminarClienteDto dto)
        {

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _cliente = await _clientesRepository.Consultar(dto.Identificacion);

                    if (_cliente is not null)
                    {
                        int personaId = _cliente.PersonaId;
                        _clientesRepository.Eliminar(_cliente.PersonaId);
                        int clientesAfectados = await _clientesRepository.Guardar();

                        _personasRepository.Eliminar(personaId);
                        int personasAfectadas = await _personasRepository.Guardar();

                        scope.Complete();
                    }
                    else
                    {
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.CLIENTE_NO_ENCONTRADO;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Eliminar() => {exc}");
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, null);
        }

    }
}
