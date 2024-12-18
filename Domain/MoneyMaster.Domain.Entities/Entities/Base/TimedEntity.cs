namespace MoneyMaster.Domain.Entities
{
    /// <summary>Сущность определенная во времени</summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TimedEntity<TKey> : Entity<TKey>, ITimedEntity<TKey>
    {
        /// <summary>Время </summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary> <inheritdoc/> </summary>
    public abstract class TimedEntity : TimedEntity<Guid>, ITimedEntity { }
}
