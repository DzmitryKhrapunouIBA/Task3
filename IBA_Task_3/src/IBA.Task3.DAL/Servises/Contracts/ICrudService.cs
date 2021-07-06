using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using System;

namespace IBA.Task3.DAL.Servises
{
    public interface ICrudService<T>
    {
        /// <summary>
        /// Получение всех сущностей.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Коллекция найденных элементов.</returns>
        Task<IEnumerable<T>> AllAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Поиск и получение сущностей.
        /// </summary>
        /// <param name="func">Функция поиска.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Коллекция найденных элементов.</returns>
        Task<IEnumerable<T>> AllAsync(Func<T, bool> func, CancellationToken token = default(CancellationToken), params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Получение единственной сущности по коду
        /// </summary>
        /// <param name="id">Код сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденный элемент.</returns>
        /// <exception cref="ArgumentNullException">Single or default throw</exception>
        /// <exception cref="InvalidOperationException">Single or defaut throw</exception>
        Task<T> GetAsync(int id, CancellationToken token = default(CancellationToken));

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
        Task<T> GetAsync(Expression<Func<T, bool>> func, CancellationToken token = default);

        /// <summary>
        /// Асинхронное создание сущности.
        /// </summary>
        /// <param name="item">Объект создаваемой сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>
        /// Возвращает объект созданной сущности.
        /// </returns>
        /// <exception cref="ArgumentNullException">If creation item is null or empty</exception>
        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Save context operation exception</exception>
        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">Save context operation exception</exception>
        Task<T> CreateAsync(T item, CancellationToken token = default(CancellationToken));
        Task<bool> UpdateAsync(T item, CancellationToken token = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken token = default(CancellationToken));
    }
}
