﻿using MoneyMaster.Common.Entities;
using MoneyMaster.Common.Interfaces.Entities;

namespace MoneyMasterService.Domain.Entities
{
    /// <summary>Счет пользователя</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class Account<TKey> : NamedTimedEntity<TKey>, ISoftDeletable
    {
        /// <summary>Баланс</summary>
        public decimal Balance { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }

        /// <summary>Идентификатор типа счета записи</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary><inheritdoc cref="Domain.Entities.AccountType"/></summary>
        public AccountType? AccountType { get; set; }


        /// <summary>Коллекция транзакций </summary>
        public ICollection<Transaction>? Transactions { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class Account : Account<Guid> { }
}
