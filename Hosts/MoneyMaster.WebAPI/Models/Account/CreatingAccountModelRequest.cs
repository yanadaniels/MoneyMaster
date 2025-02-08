using System.ComponentModel.DataAnnotations;

namespace MoneyMaster.WebAPI.Models.Account
{
    /// <summary>Модель создания счёта </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class CreatingAccountModelRequest<TKey>
    {

        /// <summary>Имя</summary>
        [Required(ErrorMessage = "Название счёта обязательно.")]
        [StringLength(50, ErrorMessage = "Название счёта не может быть длиннее 50 символов.")]
        public required string Name { get; set; }

        /// <summary>Баланс</summary>
        [Required(ErrorMessage = "Необходимо указать баланс")]
        public required decimal Balance { get; set; }

        /// <summary>Валюта</summary>
        [Required(ErrorMessage = "Необходимо указать в какой валюте будет счёт")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Валюта должна представлять собой трехбуквенный код ISO")]
        public required string Currency { get; set; }

        /// <summary>Иконка</summary>
        [Required(ErrorMessage = "Необходимо выбрать иконку")]
        public required string Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        [Required(ErrorMessage = "Необходимо указать ID пользователя который создает счёт")]
        public required TKey UserId { get; set; }

        /// <summary>Идентификатор типа счета</summary>
        [Required(ErrorMessage = "Необходимо указать ID типа счёта")]
        public required TKey AccountTypeId { get; set; }

        /// <summary>Время</summary>
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary> Модель создания счёта </summary>
    public class CreatingAccountModelRequest : CreatingAccountModelRequest<Guid>;
}
