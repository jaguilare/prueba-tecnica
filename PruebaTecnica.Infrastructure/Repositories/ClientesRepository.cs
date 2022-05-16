
using PruebaTecnica.Core.Interfaces.IRepositories;
using System;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PruebaTecnica.Core;

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

        public async Task<Cliente> Consultar(string identificacion)
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
                                select new Cliente
                                {
                                    PersonaId = clis.PersonaId,
                                    ClienteId = clis.ClienteId,
                                    Estado = clis.Estado,
                                    Persona = new Persona()
                                    {
                                        PersonaId = pers.PersonaId,
                                        Direccion = pers.Direccion,
                                        Edad = pers.Edad,
                                        Genero = pers.Genero,
                                        Identificacion = pers.Identificacion,
                                        Nombre = pers.Nombre,
                                        Telefono = pers.Telefono
                                    },
                                };
                    var cliente = await query.FirstOrDefaultAsync();
                    scope.Complete();
                    return cliente;
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

        public void Eliminar(int personaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _dbContext.Entry(new Cliente() { PersonaId = personaId }).State = EntityState.Deleted;
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
