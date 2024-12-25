// Ignore Spelling: Dto

using MoneyMaster.Services.Contracts.UserSetting;

namespace MoneyMaster.Services.Contracts.User
{
    /// <summary>Dto создания пользователя</summary>
    public class CreatingUserDto
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        /// <summary>Настройки пользователя </summary>
        public UserSettingDto? UserSetting { get; set; } = new UserSettingDto()
        {
            Language= "en-US",
            Currency= "en-US",
        };

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }  = DateTime.UtcNow;
    }
}
