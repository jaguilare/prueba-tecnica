using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Core;
using PruebaTecnica.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static PruebaTecnica.Core.Dtos.Movimientos.ReportarDto;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class MovimientosRepository : IMovimientosRepository
    {

        private readonly PruebaTecnicaContext _dbContext;

        public MovimientosRepository(PruebaTecnicaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Movimiento Actualizar(Movimiento dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _dbContext.Entry(dto).State = EntityState.Modified;
                    scope.Complete();
                    return dto;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Actualizar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<Movimiento> Consultar(int movimientoId, int cuentaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from mvts in _dbContext.Movimientos
                                where mvts.MovimientosId == movimientoId &&
                                mvts.CuentaId == cuentaId
                                select new Movimiento
                                {
                                    MovimientosId = mvts.MovimientosId,
                                    CuentaId = mvts.CuentaId,
                                    TipoMovimiento = mvts.TipoMovimiento,
                                    Fecha = mvts.Fecha,
                                    Saldo = mvts.Saldo,
                                    Valor = mvts.Valor
                                };
                    var movimiento = await query.FirstOrDefaultAsync();
                    scope.Complete();
                    return movimiento;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<IList<Movimiento>> Consultar(int cuentaId, DateTime fecha, string tipoMovimiento)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from mvts in _dbContext.Movimientos
                                where mvts.CuentaId == cuentaId &&
                                mvts.Fecha.Date == fecha.Date &&
                                mvts.TipoMovimiento == tipoMovimiento
                                orderby mvts.Fecha descending
                                select new Movimiento
                                {
                                    MovimientosId = mvts.MovimientosId,
                                    CuentaId = mvts.CuentaId,
                                    TipoMovimiento = mvts.TipoMovimiento,
                                    Fecha = mvts.Fecha,
                                    Saldo = mvts.Saldo,
                                    Valor = mvts.Valor
                                };
                    var movimientos = await query.AsQueryable().ToListAsync();
                    scope.Complete();
                    return movimientos;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<IList<Movimiento>> Consultar(int cuentaId, DateTime fecha)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from mvts in _dbContext.Movimientos
                                where mvts.CuentaId == cuentaId &&
                                mvts.Fecha.Date == fecha.Date
                                orderby mvts.Fecha descending
                                select new Movimiento
                                {
                                    MovimientosId = mvts.MovimientosId,
                                    CuentaId = mvts.CuentaId,
                                    TipoMovimiento = mvts.TipoMovimiento,
                                    Fecha = mvts.Fecha,
                                    Saldo = mvts.Saldo,
                                    Valor = mvts.Valor
                                };
                    var movimientos = await query.AsQueryable().ToListAsync();
                    scope.Complete();
                    return movimientos;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<IList<ReporteRespose>> Reportar(string identificacion, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var query = from pers in _dbContext.Personas
                            join clis in _dbContext.Clientes on pers.PersonaId equals clis.PersonaId
                            join ctas in _dbContext.Cuenta on clis.ClienteId equals ctas.ClienteId
                            join mvts in _dbContext.Movimientos on ctas.CuentaId equals mvts.CuentaId
                            where pers.Identificacion == identificacion &&
                            mvts.Fecha.Date >= fechaInicial.Date &&
                            mvts.Fecha.Date <= fechaFinal.Date
                            orderby mvts.Fecha descending
                            select new ReporteRespose
                            {
                                PersonaId = pers.PersonaId,
                                Identificacion = pers.Identificacion,
                                Nombre = pers.Nombre,
                                NumeroCuenta = ctas.NumeroCuenta,
                                TipoCuenta = ctas.TipoCuenta,
                                SaldoInicial = ctas.SaldoInicial,
                                Estado = ctas.Estado,
                                TipoMovimiento = mvts.TipoMovimiento,
                                Valor = mvts.Valor,
                                Saldo = mvts.Saldo,
                                Fecha = mvts.Fecha

                            };
                var reporte = await query.AsQueryable().ToListAsync();
                return reporte;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Consultar() => {exc}");
                throw new Exception(exc.ToString());
            }
        }

        public Movimiento Crear(Movimiento dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var movimiento = _dbContext.Movimientos.Add(dto);
                    scope.Complete();
                    return movimiento.Entity;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Crear() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public void Eliminar(int movimientoId, int cuentaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _dbContext.Entry(new Movimiento()
                    {
                        MovimientosId = movimientoId,
                        CuentaId = cuentaId,
                    }).State = EntityState.Deleted;
                    scope.Complete();
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Eliminar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<int> Guardar()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var regsAfectados = await _dbContext.SaveChangesAsync();
                    scope.Complete();
                    return regsAfectados;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Guardar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }
    }
}
