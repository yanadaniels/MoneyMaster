using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Repositories.Abstractions;
using MoneyMaster.Services.Contracts.Common;
using MoneyMaster.Infrastructure.Repositories.Implementations.Extensions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Base
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    /// <typeparam name="T"> Тип сущности. </typeparam>
    /// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
    public abstract class Repository<T, TPrimaryKey> : IRepository<T, TPrimaryKey> where T
        : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext Context;
        private readonly DbSet<T> _entitySet;

        protected Repository(DbContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }

        #region Get

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность или null если ничего найдено </returns>
        public virtual T? Get(TPrimaryKey id)
        {
            return _entitySet.Find(id);
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность или null если ничего не найдено </returns>
        public virtual async Task<T?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            return await _entitySet.FindAsync((object)id!);
        }

        /// <summary>
        /// Асинхронно получает сущность по ID, включая те, что помечены как удаленные.
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        /// <returns> Возвращает найденную сущность или null если ничего не было найдено </returns>
        public virtual async Task<T?> GetByIdIncludingDeletedAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            return await _entitySet
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e!.Id!.Equals(id), cancellationToken);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> IQueryable массив сущностей. </returns>
        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
        }

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Асинхронно получает сущности в БД с пагинацией
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking </param>
        /// <param name="parameters"> Модель передаваемых параметров для формирования пагинации </param>
        /// <returns> Возвращает кортеж со списком сущностей и общим количеством страниц </returns>
        public virtual async Task<(List<T> Items, int TotalCount)> GetAllAsync(
            PaginationParameters parameters, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = _entitySet.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            query = query.Sort(parameters.SortBy, parameters.IsDescending);
            var totalCountTask = query.CountAsync(cancellationToken);
            var itemsTask = query.Paginate(parameters.PageNumber, parameters.PageSize).ToListAsync(cancellationToken);

            await Task.WhenAll(totalCountTask, itemsTask);

            return (itemsTask.Result, totalCountTask.Result);
        }

        /// <summary>
        /// Асинхронно получает сущности помеченные как удаленные
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking </param>
        /// <param name="parameters"> Модель передаваемых параметров для формирования пагинации </param>
        /// <returns>
        /// Возвращает кортеж, где первый элемент — список удалённых сущностей, 
        /// а второй — общее количество таких сущностей.
        /// </returns>
        public virtual async Task<(List<T> Items, int TotalCount)> GetAllDeletedAsync(
            PaginationParameters parameters, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = _entitySet
                .AsQueryable()
                .IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDelete"));

            if (asNoTracking)
                query = query.AsNoTracking();

            query = query.Sort(parameters.SortBy, parameters.IsDescending);
            var totalCountTask = query.CountAsync(cancellationToken);
            var itemsTask = query.Paginate(parameters.PageNumber, parameters.PageSize).ToListAsync(cancellationToken);

            await Task.WhenAll(totalCountTask, itemsTask);

            return (itemsTask.Result, totalCountTask.Result);
        }

        #endregion

        #region Create

        /// <summary>
        /// Добавить в базу сущность.
        /// </summary>
        /// <param name="entity"> Cущность для добавления. </param>
        /// <returns>. Добавленная сущность. </returns>
        public virtual T Add(T entity)
        {
            var objToReturn = _entitySet.Add(entity);
            return objToReturn.Entity;
        }

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            return (await _entitySet.AddAsync(entity)).Entity;
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        public virtual void AddRange(List<T> entities)
        {
            var enumerable = entities as IList<T> ?? entities.ToList();
            _entitySet.AddRange(enumerable);
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }
            await _entitySet.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual bool Delete(TPrimaryKey id)
        {
            var obj = _entitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            return true;
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Сущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            Context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        /// <summary>
        /// Удалить сущности.
        /// </summary>
        /// <param name="entities"> Коллекция сущностей для удаления. </param>
        /// <returns> Была ли операция завершена успешно. </returns>
        public virtual bool DeleteRange(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return false;
            }
            _entitySet.RemoveRange(entities);
            return true;
        }

        /// <summary>
        /// Мягкое удаление сущности
        /// </summary>
        /// <param name="id"> Идентификатор удаляемой сущности </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Удалённую сущность если успешно, иначе null </returns>
        public virtual async Task<T?> SoftDeleteAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            var entity = await _entitySet.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                return null;
            }
            return await SoftDeleteAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Мягкое удаление сущности
        /// </summary>
        /// <param name="entity"> Удаляемая сущность </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Удалённую сущность если успешно, иначе null </returns>
        /// <exception cref="InvalidOperationException"> 
        /// Выбрасывается если передать сущность которая не потдерживает мягкое удаление 
        /// </exception>
        public virtual async Task<T?> SoftDeleteAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDelete = true;
                Context.Set<T>().Update(entity);
                await Context.SaveChangesAsync();
                return entity;
            }
            else
            {
                throw new InvalidOperationException($"Мягкое удаление не поддерживается для типа {typeof(T).Name}");
            }
        }

        #endregion

        #region Restore

        /// <summary>
        /// Асинхронно убирает метку удалено
        /// </summary>
        /// <param name="id"> Идентификатор восстанавливаемой сущности </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Восстановленную сущность если успешно, иначе null</returns>
        public virtual async Task<T?> RestoreAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            var entity = await _entitySet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e!.Id!.Equals(id));

            if (entity == null)
            {
                return null;
            }

            return await RestoreAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Асинхронно убирает метку удалено 
        /// </summary>
        /// <param name="entity"> Сущность которую нужно восстановить </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Восстановленную сущность если успешно, иначе null</returns>
        /// <exception cref="InvalidOperationException"> 
        /// Выбрасывается если попытались восстановить сущность которая не потдерживает мягкое удаление 
        /// </exception>
        public virtual async Task<T?> RestoreAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDelete = false;
                Context.Set<T>().Update(entity);
                await Context.SaveChangesAsync(cancellationToken);

                return entity;
            }
            else
            {
                throw new InvalidOperationException($"" +
                    $"Восстановить можно лишь те сущности которые поддерживают мягкое удаление, " +
                    $"Передаваемый тип {typeof(T).Name} не поддерживает мягкое удаление");
            }
        }

        #endregion

        #region SaveChanges

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
