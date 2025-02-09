namespace MoneyMasterService.WebAPI.Models.User
{
    /// <summary>Модель создания пользователя</summary>
    public class CreatingUserModel
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        ///// <summary>Настройки пользователя </summary>
        //public UserSettingModel? UserSetting { get; set; } = new UserSettingModel()
        //{
        //    Language = "en-US",
        //    Currency = "en-US",
        //};

        /// <summary>Время</summary>
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }
}
