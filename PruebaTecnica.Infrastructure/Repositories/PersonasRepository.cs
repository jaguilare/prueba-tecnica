
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Core;
using PruebaTecnica.Core.Interfaces.IRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class PersonasRepository : IPersonasRepository
    {
        private readonly PruebaTecnicaContext _dbContext;

        public PersonasRepository(PruebaTecnicaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Persona> Consultar(string identificacion)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var p = await _dbContext.Personas.FirstOrDefaultAsync(p => p.Identificacion == identificacion);
                    scope.Complete();
                    return p;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public Persona Crear(Persona dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var p = _dbContext.Personas.Add(dto);
                    scope.Complete();
                    return p.Entity;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Crear() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public Persona Actualizar(Persona dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var p = _dbContext.Personas.Update(dto);
                    scope.Complete();
                    return p.Entity;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Actualizar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public Persona Eliminar(Persona dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var p = _dbContext.Personas.Remove(dto);
                    scope.Complete();
                    return p.Entity;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Eliminar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<int> GuardarCambiosOk()
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
                    Console.WriteLine($"GuardarCambiosOk() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

    }
}
