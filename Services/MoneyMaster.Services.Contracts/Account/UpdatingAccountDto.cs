using System.ComponentModel.DataAnnotations;

namespace MoneyMaster.Services.Contracts.Account
{
    /// <summary>Модель для редактирования счёта </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class UpdatingAccountDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public required TKey Id { get; set; }

        /// <summary>Имя</summary>
        public string? Name { get; set; }

        /// <summary>Баланс</summary>
        public decimal? Balance { get; set; }

        /// <summary>Валюта</summary>
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Идентификатор типа счета</summary>
        public TKey? AccountTypeId { get; set; }
    }

    /// <summary>Модель для редактирования счёта</summary>
    public class UpdatingAccountDto : UpdatingAccountDto<Guid>;
}
