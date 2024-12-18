namespace MoneyMaster.Domain.Entities
{
    /// <summary>Именованная сущность</summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class NamedEntity<TKey> : Entity<TKey>, INamedEntity<TKey>
    {
        /// <summary>Имя</summary>
        public required string Name { get; set; }

    }

    /// <summary> <inheritdoc/> </summary>
    public abstract class NamedEntity : NamedEntity<Guid>, INamedEntity { }
}
