// Ignore Spelling: Auth

namespace MoneyMaster.Common.Options
{
    public class AuthOptions
    {
        public const string Position = "Authorization:AditionalOptions";
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Потребитель токена
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Ключ для шифрации
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// время жизни токена в минутах
        /// </summary>
        public int LifeTime { get; set; }
    }
}
