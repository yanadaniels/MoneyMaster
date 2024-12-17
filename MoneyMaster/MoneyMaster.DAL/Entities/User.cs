using MoneyMaster.DAL.Entities.Base;

namespace MoneyMaster.DAL.Entities
{
    public class User: TimedEntity
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }
    }
}
