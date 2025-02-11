// Ignore Spelling: Dto

namespace MoneyMasterService.Services.Contracts.Account
{
    /// <summary>Модель счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public record CreatingAccountDto<TKey>
    {
        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Баланс</summary>
        public required decimal Balance { get; set; }

        /// <summary>Валюта</summary>
        public required string Currency { get; set; }

        /// <summary>Иконка</summary>
        public required string Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public required TKey UserId { get; set; }

        /// <summary>Идентификатор типа счета</summary>
        public required TKey AccountTypeId { get; set; }

        /// <summary>Время</summary>
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }

    public record CreatingAccountDto : CreatingAccountDto<Guid>;
}
