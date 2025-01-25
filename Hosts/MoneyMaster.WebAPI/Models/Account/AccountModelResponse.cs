namespace MoneyMaster.WebAPI.Models.Account
{
    /// <summary>Модель счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class AccountModelResponse<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Баланс</summary>
        public decimal Balance { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Идентификатор типа счета</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель счета </summary>
    public class AccountModelResponse : AccountModelResponse<Guid>;
}
