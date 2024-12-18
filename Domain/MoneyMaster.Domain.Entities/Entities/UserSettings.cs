namespace MoneyMaster.Domain.Entities
{
    public class UserSettings<TKey> : Entity<Guid>
    {
        /// <summary>Идентификатор пользователя</summary>
        public required TKey UserId { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Язык</summary>
        public string? Language { get; set; }
    }

    /// <summary> <inheritdoc/></summary>
    public class UserSettings : UserSettings<Guid> { }
}
