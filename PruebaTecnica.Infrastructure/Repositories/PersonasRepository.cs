
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
                    var persona = await _dbContext.Personas.FirstOrDefaultAsync(p => p.Identificacion == identificacion);
                    scope.Complete();
                    return persona;
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Consultar() => {exc}");
                    throw new Exception(exc.ToString());
                }
            }
        }

        public async Task<Persona> ConsultarPersonaCliente(string identificacion)
        {

            try
            {
                var query = from pers in _dbContext.Personas
                            join clis in _dbContext.Clientes on pers.PersonaId equals clis.PersonaId
                            where pers.Identificacion == identificacion
                            select new Persona
                            {
                                PersonaId = pers.PersonaId,
                                Identificacion = pers.Identificacion,
                                Nombre = pers.Nombre,
                                Edad = pers.Edad,
                                Genero = pers.Genero,
                                Telefono = pers.Telefono,
                                Direccion = pers.Direccion,
                                Cliente = new Cliente()
                                {
                                    ClienteId = clis.ClienteId,
                                    PersonaId = pers.PersonaId,
                                    Contrasenia = clis.Contrasenia,
                                    Estado = clis.Estado
                                }
                            };
                var clientes = await query.AsQueryable().ToListAsync();
                return clientes.FirstOrDefault();
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Consultar() => {exc}");
                throw new Exception(exc.ToString());
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

        public void Eliminar(int personaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    _dbContext.Entry(new Persona() { PersonaId = personaId }).State = EntityState.Deleted;
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
