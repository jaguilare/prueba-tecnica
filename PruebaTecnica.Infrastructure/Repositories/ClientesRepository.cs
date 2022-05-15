
using PruebaTecnica.Core;
using PruebaTecnica.Core.Interfaces.IRepositories;
using System;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class ClientesRepository : IClientesRepository
    {

        private readonly PruebaTecnicaContext _dbContext;

        public ClientesRepository(PruebaTecnicaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Cliente Actualizar(Cliente dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Cliente>> Consultar(string identificacion)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var query = from pers in _dbContext.Personas
                                join clis in _dbContext.Clientes on pers.PersonaId equals clis.PersonaId
                                where pers.Identificacion == identificacion
                                select new Cliente { PersonaId = pers.PersonaId };

                    var clientes = await query.AsQueryable().ToListAsync();
                    scope.Complete();
                    return clientes;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }

        }

        public Cliente Crear(Cliente dto)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var cliente = _dbContext.Clientes.Add(dto);
                    scope.Complete();
                    return cliente.Entity;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Crear() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public Cliente Eliminar(Cliente dto)
        {
            throw new NotImplementedException();
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
