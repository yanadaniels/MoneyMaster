namespace MoneyMaster.Domain.Entities
{
    /// <summary>Настройки пользователей </summary>
    /// <typeparam name="TKey">Первичный ключ</typeparam>
    public class UserSetting<TKey> : Entity<Guid>
    {
        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary><inheritdoc cref="MoneyMaster.Domain.Entities.User"/></summary>
        public User? User { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Язык</summary>
        public string? Language { get; set; }
    }

    /// <summary> <inheritdoc/></summary>
    public class UserSetting : UserSetting<Guid> { }
}
