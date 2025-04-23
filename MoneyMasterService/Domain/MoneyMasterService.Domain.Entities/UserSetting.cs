using MoneyMaster.Common.Entities;
using MoneyMaster.Common.Interfaces.Entities;

namespace MoneyMasterService.Domain.Entities
{
    /// <summary>Настройки пользователей </summary>
    /// <typeparam name="TKey">Первичный ключ</typeparam>
    public class UserSetting<TKey> : Entity<Guid>, ISoftDeletable
    {
        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Валюта</summary>
        public string? Currency { get; set; }

        /// <summary>Язык</summary>
        public string? Language { get; set; }
        public bool IsDeleted { get; set; }
    }

    /// <summary> <inheritdoc/></summary>
    public class UserSetting : UserSetting<Guid> { }
}
