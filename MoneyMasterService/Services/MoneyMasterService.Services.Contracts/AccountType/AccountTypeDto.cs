// Ignore Spelling: Dto

using MoneyMasterService.Services.Contracts.Account;

namespace MoneyMasterService.Services.Contracts.AccountType
{
    /// <summary>DTO Тип счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class AccountTypeDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<AccountDto>? Accounts { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO Тип счета </summary>
    public class AccountTypeDto: AccountTypeDto<Guid>;
}
