using MoneyMaster.WebAPI.Models.AccountType;

namespace MoneyMaster.WebAPI.Models.Account
{
    /// <summary>Модель создания счёта</summary>
    public class CreatingAccountInfoModelResponse
    {
        /// <summary>Имя пользователя</summary>
        public required ICollection<AccountTypeModel> AccountTypes { get; set; }
        public required ICollection<String> CurrentCode { get; set; }
        //public required ICollection<String> Icons { get; set; }
    }
}
