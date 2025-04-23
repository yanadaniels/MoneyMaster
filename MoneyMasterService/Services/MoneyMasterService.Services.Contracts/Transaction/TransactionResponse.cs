namespace MoneyMasterService.Services.Contracts.Transaction
{
    /// <summary>модель ответа транзакции</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public record TransactionResponse<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Количество</summary>
        public decimal Amount { get; set; }

        /// <summary>Идентификатор категории</summary>
        public TKey? CategoryId { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Идентификатор счета</summary>
        public TKey? AccountId { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO транзакции</summary>
    public record TransactionResponse : TransactionResponse<Guid>;
}
