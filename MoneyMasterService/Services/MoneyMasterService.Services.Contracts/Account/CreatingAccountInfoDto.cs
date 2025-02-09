// Ignore Spelling: Dto

using MoneyMasterService.Services.Contracts.AccountType;

namespace MoneyMasterService.Services.Contracts.Account
{
    /// <summary>
    /// Данные необходимые для создания нового счёта
    /// </summary>
    public record CreatingAccountInfoDto
    {
        public required ICollection<AccountTypeDto> AccountTypes { get; set; }
        public required ICollection<string> CurrentCode { get; set; }
        //public required ICollection<string> Icons { get; set; }
    }
}
