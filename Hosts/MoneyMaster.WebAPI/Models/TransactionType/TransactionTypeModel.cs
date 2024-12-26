﻿using MoneyMaster.WebAPI.Models.Category;
using MoneyMaster.WebAPI.Models.Transaction;

namespace MoneyMaster.WebAPI.Models.TransactionType
{
    /// <summary>Модель типа транзакции</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class TransactionTypeModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Коллекция категорий</summary>
        public ICollection<CategoryModel>? Categories { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<TransactionModel>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель типа транзакции</summary>
    public class TransactionTypeModel : TransactionTypeModel<Guid>;
}
