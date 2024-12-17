namespace MoneyMaster.DAL.Interfaces
{
    /// <summary>Сущность</summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>Идентификатор</summary>
        TKey Id { get; }
    }

    /// <summary>Сущность</summary>
    public interface IEntity : IEntity<Guid> { }
}
