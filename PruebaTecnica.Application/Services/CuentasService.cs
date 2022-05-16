using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Core;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Request.Cuentas;
using PruebaTecnica.Core.Values;
using PruebaTecnica.Core.Interfaces.IRepositories;
using PruebaTecnica.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using PruebaTecnica.Core.Dtos.Movimientos;

namespace PruebaTecnica.Application.Services
{
    public class CuentasService : ICuentasService
    {

        private Cuentum _cuenta;
        private IList<Cuentum> _cuentas;
        private string _mensajeRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        private readonly ICuentasRepository _cuentasRepository;
        private readonly IMovimientosRepository _movimientosRepository;


        public CuentasService(
            ICuentasRepository cuentasRepository,
            IMovimientosRepository movimientosRepository)
        {
            _cuenta = null;
            _cuentas = new List<Cuentum>();
            _mensajeRespuesta = MensajesRespuesta.OK;
            _codigoRespuesta = ECodigoRespuesta.OK;
            _cuentasRepository = cuentasRepository;
            _movimientosRepository = movimientosRepository;
        }

        public async Task<Respuesta> Consultar(ConsultarCuentasDto dto)
        {
            try
            {
                _cuentas = await _cuentasRepository.Consultar(dto.ClienteId);

                if (_cuentas.Count == 0)
                {
                    _mensajeRespuesta = MensajesRespuesta.CUENTA_NO_ENCONTRADA;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Consultar() => {exc}");
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _mensajeRespuesta = MensajesRespuesta.ERROR;
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, ConsultarCuentasDto.Response(_cuentas));
        }

        public async Task<Respuesta> Consultar(ConsultarCuentaDto dto)
        {
            try
            {
                _cuenta = await _cuentasRepository.Consultar(dto.CuentaId, dto.NumeroCuenta, dto.ClienteId);

                if (_cuentas.Count == 0)
                {
                    _mensajeRespuesta = MensajesRespuesta.CUENTA_NO_ENCONTRADA;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Consultar() => {exc}");
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _mensajeRespuesta = MensajesRespuesta.ERROR;
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, _cuenta);
        }

        public async Task<Respuesta> Crear(CrearCuentaDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _cuenta = await _cuentasRepository.Consultar(dto.Cuenta.NumeroCuenta, dto.Cuenta.ClienteId);

                    if (_cuenta is null)
                    {
                        _cuenta = _cuentasRepository.Crear(dto.Cuenta);
                        int cuentasAfectadas = await _cuentasRepository.Guardar();

                        Movimiento movimiento = new Movimiento()
                        {
                            CuentaId = _cuenta.CuentaId,
                            Fecha = DateTime.Now,
                            TipoMovimiento = TiposMovimientos.CREDITO,
                            Saldo = dto.Cuenta.SaldoInicial,
                            Valor = dto.Cuenta.SaldoInicial
                        };

                        _movimientosRepository.Crear(new CrearMovimientoDto().Movimiento = movimiento);
                        int mvtsAfectadas = await _movimientosRepository.Guardar();

                        scope.Complete();
                    }
                    else
                    {
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.CUENTA_YA_EXISTE;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Crear() => {exc}");
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, CrearCuentaDto.Response(_cuenta));
        }

        public async Task<Respuesta> Actualizar(ActualizarCuentaDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _cuenta = await _cuentasRepository.Consultar(dto.Cuenta.CuentaId, dto.Cuenta.NumeroCuenta, dto.Cuenta.ClienteId);

                    if (_cuenta is not null)
                    {
                        _cuenta = _cuentasRepository.Actualizar(dto.Cuenta);
                        int personasAfectadas = await _cuentasRepository.Guardar();
                        scope.Complete();
                    }
                    else
                    {
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.CUENTA_NO_ENCONTRADA;
                    }
                }
                catch (Exception exc)
                {
                    _cuenta = null;
                    Console.WriteLine($"Actualizar() => {exc}");
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = MensajesRespuesta.ERROR;
                }
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, null);
        }

        public async Task<Respuesta> Eliminar(EliminarCuentaDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                    TransactionScopeAsyncFlowOption.Enabled
                ))
            {
                try
                {
                    _cuenta = await _cuentasRepository.Consultar(dto.CuentaId, dto.NumeroCuenta, dto.ClienteId);

                    if (_cuenta is not null)
                    {
                        _cuentasRepository.Eliminar(_cuenta.NumeroCuenta, _cuenta.ClienteId);
                        int clientesAfectados = await _cuentasRepository.Guardar();
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
