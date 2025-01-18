using MoneyMaster.Domain.Entities.Enums;
using MoneyMaster.Services.Contracts.AccountType;

namespace MoneyMaster.Services.Contracts.Account
{
    /// <summary>
    /// Данные необходимые для создания нового счёта
    /// </summary>
    public class CreatingAccountInfoDto
    {
        public required ICollection<AccountTypeDto> AccountTypes { get; set; }
        public required ICollection<String> CurrentCode { get; set; }
        //public required ICollection<String> Icons { get; set; }
    }
}
