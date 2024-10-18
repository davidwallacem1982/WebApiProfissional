using WebApiProfissional.Domain.Interfaces.Repository.Base;
using WebApiProfissional.Infra.Configurations.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApiProfissional.Infra.Repository.Base
{
    /// <summary>
    /// Classe base genérica para repositórios, implementando operações de CRUD (Create, Read, Update, Delete),
    /// tanto de forma síncrona quanto assíncrona. Esta classe utiliza o Entity Framework para acessar e manipular dados
    /// no banco de dados.
    /// </summary>
    /// <typeparam name="T">O tipo de entidade que este repositório gerencia.</typeparam>
    /// <remarks>
    /// Inicializa uma nova instância do repositório com o contexto fornecido.
    /// </remarks>
    /// <param name="context">O contexto do banco de dados utilizado pelo repositório.</param>
    /// <exception cref="ArgumentNullException">Lançada quando o contexto é nulo.</exception>
    public class RepositoryBase<T>(IntegrationContext context) : IDisposable, IRepositoryBase<T> where T : class
    {
        private readonly IntegrationContext context = context ?? throw new ArgumentNullException(nameof(context));

        #region Métodos Síncronos

        public DbContext GetContext() => context;
        
        public T Add(T entity) => context.Add(entity).Entity;

        public void Update(T entity) => context.Entry(entity).State = EntityState.Modified;

        public T Remove(T entity) => context.Remove(entity).Entity;

        public T GetById(int id) => context.Find<T>(id);

        public T GetFirstOrDefaultBy(Expression<Func<T, bool>> predicate) => context.Set<T>().FirstOrDefault(predicate);

        public T GetSingleOrDefaultBy(Expression<Func<T, bool>> predicate) => context.Set<T>().SingleOrDefault(predicate);

        public T GetAsNoTracking(Expression<Func<T, bool>> predicate) =>
            context.Set<T>().AsNoTracking().FirstOrDefault(predicate);

        public IQueryable<T> GetList() => context.Set<T>().AsQueryable();

        public IQueryable<T> GetListBy(Expression<Func<T, bool>> predicate) =>
            context.Set<T>().Where(predicate).AsQueryable();

        public bool Exist(Expression<Func<T, bool>> predicate) =>
            context.Set<T>().Any(predicate);

        public long Count() => context.Set<T>().Count();

        public long CountBy(Expression<Func<T, bool>> predicate) =>
            context.Set<T>().Where(predicate).Count();

        public bool SaveChanges() => context.SaveChanges() > 0;

        #endregion

        #region Métodos Assíncronos

        public async Task<T> GetByIdAsync(int id) => await context.FindAsync<T>(id);

        public async Task<T> GetFirstOrDefaultAsyncBy(Expression<Func<T, bool>> predicate) =>
            await context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetSingleOrDefaultAsyncBy(Expression<Func<T, bool>> predicate) =>
            await context.Set<T>().SingleOrDefaultAsync(predicate);

        public async Task<T> GetAsNoTrackingAsync(Expression<Func<T, bool>> predicate) =>
            await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetListAsync() =>
            await context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> GetListByAsync(Expression<Func<T, bool>> predicate) =>
            await context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate) =>
            await context.Set<T>().AnyAsync(predicate);

        public async Task<long> CountAsync() => await context.Set<T>().AsNoTracking().CountAsync();

        public async Task<long> CountByAsync(Expression<Func<T, bool>> predicate) =>
            await context.Set<T>().Where(predicate).AsNoTracking().CountAsync();

        public async Task<bool> SaveChangesAsync() =>
            await context.SaveChangesAsync() > 0;

        #endregion

        /// <summary>
        /// Libera os recursos utilizados pelo repositório.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
