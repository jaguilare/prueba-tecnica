using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Core;
using PruebaTecnica.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class CuentasRepository : ICuentasRepository
    {

        private readonly PruebaTecnicaContext _dbContext;

        public CuentasRepository(PruebaTecnicaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cuentum Actualizar(Cuentum dto)
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

        public async Task<Cuentum> Consultar(int cuentaId, string numeroCuenta, int clienteId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from cuets in _dbContext.Cuenta
                                where cuets.CuentaId == cuentaId &&
                                cuets.NumeroCuenta == numeroCuenta &&
                                cuets.ClienteId == clienteId
                                select new Cuentum
                                {
                                    ClienteId = cuets.ClienteId,
                                    CuentaId = cuets.CuentaId,
                                    Estado = cuets.Estado,
                                    NumeroCuenta = cuets.NumeroCuenta,
                                    TipoCuenta = cuets.TipoCuenta,
                                    SaldoInicial = cuets.SaldoInicial
                                };
                    var cuenta = await query.FirstOrDefaultAsync();
                    scope.Complete();
                    return cuenta;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<Cuentum> Consultar(string numeroCuenta, int clienteId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from cuets in _dbContext.Cuenta
                                where cuets.NumeroCuenta == numeroCuenta &&
                                cuets.ClienteId == clienteId
                                select new Cuentum
                                {
                                    ClienteId = cuets.ClienteId,
                                    CuentaId = cuets.CuentaId,
                                    Estado = cuets.Estado,
                                    NumeroCuenta = cuets.NumeroCuenta,
                                    TipoCuenta = cuets.TipoCuenta,
                                    SaldoInicial = cuets.SaldoInicial
                                };
                    var cuenta = await query.FirstOrDefaultAsync();
                    scope.Complete();
                    return cuenta;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<IList<Cuentum>> Consultar(int clienteId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from cuets in _dbContext.Cuenta
                                where cuets.ClienteId == clienteId
                                select new Cuentum
                                {
                                    ClienteId = cuets.ClienteId,
                                    CuentaId = cuets.CuentaId,
                                    Estado = cuets.Estado,
                                    NumeroCuenta = cuets.NumeroCuenta,
                                    TipoCuenta = cuets.TipoCuenta,
                                    SaldoInicial = cuets.SaldoInicial
                                };
                    var cuentas = await query.AsQueryable().ToListAsync();
                    scope.Complete();
                    return cuentas;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public Cuentum Crear(Cuentum dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var cuenta = _dbContext.Cuenta.Add(dto);
                    scope.Complete();
                    return cuenta.Entity;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Crear() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public void Eliminar(string numeroCuenta, int clienteId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _dbContext.Entry(new Cuentum()
                    {
                        NumeroCuenta = numeroCuenta,
                        ClienteId = clienteId,
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
