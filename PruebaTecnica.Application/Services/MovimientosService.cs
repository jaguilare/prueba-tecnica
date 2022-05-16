using PruebaTecnica.Core;
using PruebaTecnica.Core.Dtos.App;
using PruebaTecnica.Core.Dtos.Movimientos;
using PruebaTecnica.Core.Interfaces.IRepositories;
using PruebaTecnica.Core.Interfaces.Services;
using PruebaTecnica.Core.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static PruebaTecnica.Core.Dtos.Movimientos.ReportarDto;

namespace PruebaTecnica.Application.Services
{
    public class MovimientosService : IMovimientosService
    {

        private Persona _persona;
        private Cliente _cliente;
        private Movimiento _movimiento;
        private IList<Movimiento> _movimientos;
        private string _mensajeRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        private readonly IMovimientosRepository _movimientosRepository;
        private readonly IClientesRepository _clientesRepository;


        public MovimientosService(
            IMovimientosRepository movimientosRepository,
            IClientesRepository clienesRepository
            )
        {
            _movimientos = new List<Movimiento>();
            _mensajeRespuesta = MensajesRespuesta.OK;
            _codigoRespuesta = ECodigoRespuesta.OK;
            _movimientosRepository = movimientosRepository;
            _clientesRepository = clienesRepository;
        }

        public async Task<Respuesta> Actualizar(ActualizarMovimientoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _movimiento = await _movimientosRepository.Consultar(dto.Movimiento.MovimientosId, dto.Movimiento.CuentaId);

                    if (_movimiento is not null)
                    {
                        _movimientosRepository.Actualizar(dto.Movimiento);
                        int mvtsAfectados = await _movimientosRepository.Guardar();
                        scope.Complete();
                    }
                    else
                    {
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.MOVIMIENTO_NO_ENCONTRADO;
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

        public Task<Respuesta> Consultar(ConsultarMovimientoDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta> Crear(CrearMovimientoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    //  BUSCAR LA CUENTA PRIMERO

                    dto.Movimiento.Fecha = DateTime.Now;

                    IList<Movimiento> movimientosDebito = await _movimientosRepository
                        .Consultar(dto.Movimiento.CuentaId, dto.Movimiento.Fecha, TiposMovimientos.DEBITO);

                    decimal debitosDia = movimientosDebito.Select(m => m.Valor).Sum();

                    if (dto.Movimiento.TipoMovimiento == TiposMovimientos.DEBITO
                        &&
                        (debitosDia >= Rangos.LIMITE_DOLARES_DIARIO_TRANSACCIONAL
                        || dto.Movimiento.Valor >= Rangos.LIMITE_DOLARES_DIARIO_TRANSACCIONAL))
                    {
                        throw new Exception(MensajesRespuesta.MOVIMIENTO_CUPO_DIARIO_EXCEDIDO);
                    }

                    IList<Movimiento> movimientos = await _movimientosRepository
                        .Consultar(dto.Movimiento.CuentaId, dto.Movimiento.Fecha);

                    var movimientosHoy = movimientos.FirstOrDefault();
                    if (movimientosHoy != null)
                    {
                        decimal ultimoSaldo = movimientosHoy.Saldo;

                        if (dto.Movimiento.TipoMovimiento == TiposMovimientos.DEBITO
                            &&
                            (ultimoSaldo == 0 || ultimoSaldo < dto.Movimiento.Valor))
                        {
                            throw new Exception(MensajesRespuesta.MOVIMIENTO_SALDO_NO_DISPONIBLE);
                        }

                        if (dto.Movimiento.TipoMovimiento == TiposMovimientos.CREDITO)
                        {
                            dto.Movimiento.Saldo = ultimoSaldo + dto.Movimiento.Valor;
                        }
                        else
                        {
                            dto.Movimiento.Saldo = ultimoSaldo - dto.Movimiento.Valor;
                        }
                    }

                    _movimiento = _movimientosRepository.Crear(dto.Movimiento);
                    int mvtsAfectadas = await _movimientosRepository.Guardar();
                    scope.Complete();
                }
                catch (Exception exc)
                {
                    _cliente = null;
                    Console.WriteLine($"Crear() => {exc}");
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = exc.Message;
                    return new Respuesta(_mensajeRespuesta, _codigoRespuesta, null);
                }

                return new Respuesta(_mensajeRespuesta, _codigoRespuesta, CrearMovimientoDto.Response(_movimiento));
            }

        }

        public async Task<Respuesta> Eliminar(EliminarMovimientoDto dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _movimiento = await _movimientosRepository.Consultar(dto.MovimientoId, dto.CuentaId);

                    if (_movimiento is not null)
                    {
                        _movimientosRepository.Eliminar(_movimiento.MovimientosId, _movimiento.CuentaId);
                        int clientesAfectados = await _movimientosRepository.Guardar();
                        scope.Complete();
                    }
                    else
                    {
                        _codigoRespuesta = ECodigoRespuesta.ERROR;
                        _mensajeRespuesta = MensajesRespuesta.MOVIMIENTO_NO_ENCONTRADO;
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

        public async Task<Respuesta> Reportar(ReportarDto dto)
        {
            IList<ReporteRespose> reporte = new List<ReporteRespose>();
            try
            {
                reporte = await _movimientosRepository
                   .Reportar(dto.Identificacion, dto.FechaInicial, dto.FechaFinal);

                if (_movimientos.Count == 0)
                {
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _mensajeRespuesta = MensajesRespuesta.REPORTE_SIN_DATOS;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Eliminar() => {exc}");
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _mensajeRespuesta = MensajesRespuesta.REPORTE_ERROR;
            }

            return new Respuesta(_mensajeRespuesta, _codigoRespuesta, reporte);

        }
    }
}
