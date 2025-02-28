using MoneyMaster.APIgateway.Models.AccountType;
using System.Text.Json.Serialization;

namespace MoneyMaster.APIgateway.Models.Account;

/// <summary>Модель создания счёта</summary>
public class CreatingAccountInfoModelResponse
{
    /// <summary>Имя пользователя</summary>
    [JsonPropertyName("accountTypes")]
    public required ICollection<AccountTypeModel> AccountTypes { get; set; }

    /// <summary>Коды валют</summary>
    [JsonPropertyName("currentCode")]
    public required ICollection<String> CurrentCode { get; set; }
    //public required ICollection<String> Icons { get; set; }
}
