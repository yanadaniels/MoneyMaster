using System.Text.Json.Serialization;

namespace MoneyMaster.APIgateway.Models.AccountType
{
    /// <summary>Модель Типа счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class AccountTypeModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        [JsonPropertyName("id")]
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        /// <summary>Признак того что тип системный</summary>
        [JsonPropertyName("isSystem")]
        public bool IsSystem { get; set; }

        /// <summary>Мягкое удаление</summary>
        [JsonPropertyName("isDelete")]
        public bool IsDelete { get; set; }

        /// <summary>Время</summary>
        [JsonPropertyName("createAt")]
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель Типа счета </summary>
    public class AccountTypeModel : AccountTypeModel<Guid>;
}
