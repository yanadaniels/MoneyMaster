using MoneyMaster.DAL.Interfaces;

namespace MoneyMaster.DAL.Entities.Base
{
    /// <summary>Сущность</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public required TKey Id { get; set; }
    }

    public abstract class Entity : Entity<Guid> { } 
}
