using MoneyMaster.WebAPI.Models.Account;

namespace MoneyMaster.WebAPI.Models.AccountType
{
    /// <summary>Модель Типа счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class AccountTypeModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<AccountModel>? Accounts { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель Типа счета </summary>
    public class AccountTypeModel : AccountTypeModel<Guid>;
}
