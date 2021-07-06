using IBA.Task3.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IBA.Task3.DAL.Servises
{
    public abstract class AbstractService<TContext, T> : BaseService, ICrudService<T>
        where TContext : DbContext
        where T : class, IEntity
    {
        protected TContext Context { get; }

        protected IQueryable<T> Entry { get; }

        public AbstractService(TContext context) : base() {

            Context = context;

            Entry = Context.Set<T>().AsQueryable<T>().AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> AllAsync(CancellationToken token = default)
        {
            InitTokenThrow(token);

            return await Entry.ToListAsync(token);
        }

        public virtual async Task<IEnumerable<T>> AllAsync(Func<T, bool> func, CancellationToken token = default, params Expression<Func<T, object>>[] includes)
        {
            InitTokenThrow(token);

            var query = Entry;
            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (x, y) => x.Include(y));

            if (func != null)
                query = (IQueryable<T>)query.Where(func);

            return await query.ToListAsync(token);
        }

        /// <summary>
        /// Асинхронное создание сущности.
        /// </summary>
        /// <param name="item">Объект создаваемой сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>
        /// Возвращает объект созданной сущности.
        /// </returns>
        /// <exception cref="ArgumentNullException">If creation item is null or empty</exception>
        /// <exception cref="DbUpdateException">Save context operation exception</exception>
        /// <exception cref="DbUpdateConcurrencyException">Save context operation exception</exception>
        public virtual async Task<T> CreateAsync(T item, CancellationToken token = default)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(T));

                var entry = Context.Set<T>().Add(item);
                await Context.SaveChangesAsync(token);

                return entry.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async virtual Task<bool> DeleteAsync(int id, CancellationToken token = default)
        {
            try
            {

                var entry = await Context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
                if (entry == null)
                    return false;

                Context.Set<T>().Remove(entry);
                await Context.SaveChangesAsync(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Получение единственной сущности по коду
        /// </summary>
        /// <param name="id">Код сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>
        /// Найденный элемент.
        /// </returns>
        /// <exception cref="ArgumentNullException">Single or default throw</exception>
        /// <exception cref="InvalidOperationException">Single or defaut throw</exception>
        public virtual async Task<T> GetAsync(int id, CancellationToken token = default)
        {
            var a = Entry.SingleOrDefaultAsync(x => x.Id == id, token);
            return await Entry.SingleOrDefaultAsync(x => x.Id == id, token);
        }

        /// <summary>
        /// Асинхронное получение единственного элемента по выражению
        /// </summary>
        /// <param name="func">Функция поиска элемента.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>
        /// Возвращает найденный элемент.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throw at single or default </exception>
        /// <exception cref="InvalidOperationException">Throw at single or default </exception>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> func, CancellationToken token = default)
        {
            return await Entry.SingleOrDefaultAsync(func, token);
        }

        public async virtual Task<bool> UpdateAsync(T item, CancellationToken token = default)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(T));

                var entry = Context.Set<T>().Update(item);
                await Context.SaveChangesAsync(token);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
