using System.ComponentModel.DataAnnotations;

namespace MoneyMaster.WebAPI.Models.Account
{
    /// <summary>Модель для редактирования счёта</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class UpdatingAccountModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        [Required(ErrorMessage = "Идентификатор счёта обязателен")]
        public required TKey Id { get; set; }

        /// <summary>Имя</summary>
        [StringLength(50, ErrorMessage = "Название счёта не может быть длиннее 50 символов.")]
        public string? Name { get; set; }

        /// <summary>Баланс</summary>
        [Range(0, double.MaxValue, ErrorMessage = "Баланс не может быть меньше нуля.")]
        public decimal? Balance { get; set; }

        /// <summary>Валюта</summary>
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Валюта должна представлять собой трехбуквенный код ISO")]
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор типа счета</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }
    }

    /// <summary>Модель для редактирования счёта</summary>
    public class UpdatingAccountModel : UpdatingAccountModel<Guid>;
}
