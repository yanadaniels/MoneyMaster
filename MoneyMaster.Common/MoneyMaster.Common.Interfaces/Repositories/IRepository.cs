using MoneyMaster.Common.Interfaces.Entities;

namespace MoneyMaster.Common.Interfaces.Repositories
{
    /// <summary>
    /// Описания общих методов для всех репозиториев.
    /// </summary>
    /// <typeparam name="T"> Тип Entity для репозитория. </typeparam>
    /// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
    public interface IRepository<T, TPrimaryKey>
        where T : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="noTracking"> Вызвать с AsNoTracking.</param>
        /// <returns> IQueryable массив сущностей.</returns>
        IQueryable<T> GetAll(bool noTracking = false);

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

        /// <summary>
        /// Асинхронно получает сущности с пагинацией
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с ApNoTracking </param>
        /// <param name="parameters"> Модель передаваемых параметров для формирования пагинации </param>
        /// <returns> Возвращает кортеж со списком сущностей и общим количеством страниц </returns>
        Task<(List<T> Items, int TotalCount)> GetAllAsync(
            PaginationParameters parameters, CancellationToken cancellationToken, bool asNoTracking = false);

        /// <summary>
        /// Асинхронно получает сущности помеченные как удаленные c пагинацией
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с ApNoTracking </param>
        /// <param name="parameters"> Модель передаваемых параметров для формирования пагинации </param>
        /// <returns> Возвращает кортеж со список сущностей помеченных как удаленные и общим количеством страниц </returns>
        Task<(List<T> Items, int TotalCount)> GetAllDeletedAsync(
            PaginationParameters parameters, CancellationToken cancellationToken, bool asNoTracking = false);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность или null если ничего найдено </returns>
        T? Get(TPrimaryKey id);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность или null если ничего найдено </returns>
        Task<T?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно получает сущность по ID, включая те, что помечены как удаленные.
        /// </summary>
        /// <param name="id"> Идентификатор сущности </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Возвращает найденную сущность или null если ничего не было найдено </returns>
        Task<T?> GetByIdIncludingDeletedAsync(TPrimaryKey id, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно помечает сущность как удаленную
        /// </summary>
        /// <param name="id"> Индентификатор сущности </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Удалённую сущность если успешно, иначе null </returns>
        Task<T?> SoftDeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно помечает сущность как удаленную
        /// </summary>
        /// <param name="entity"> Сущность которую нужно пометить как удаленную </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Удалённую сущность если успешно, иначе null </returns>
        Task<T?> SoftDeleteAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(TPrimaryKey id);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(T entity);

        /// <summary>
        /// Удалить сущности.
        /// </summary>
        /// <param name="entities"> Коллекция сущностей для удаления. </param>
        /// <returns> Была ли операция удаления успешна. </returns>
        bool DeleteRange(ICollection<T> entities);

        /// <summary>
        /// Асинхронно убирает метку удалено
        /// </summary>
        /// <param name="id"> Идентификатор восстанавливаемой сущности </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Восстановленную сущность если успешно, иначе null</returns>
        Task<T?> RestoreAsync(TPrimaryKey id, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно убирает метку удалено
        /// </summary>
        /// <param name="entity"> Сущность которую нужно восстановить </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Восстановленную сущность если успешно, иначе null</returns>
        Task<T?> RestoreAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        void Update(T entity);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        T Add(T entity);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        void AddRange(List<T> entities);

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        Task AddRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
