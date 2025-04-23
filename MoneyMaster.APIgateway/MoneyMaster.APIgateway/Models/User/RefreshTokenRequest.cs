namespace MoneyMaster.APIgateway.Models.User
{
    /// <summary>Модель обновления access token</summary>
    public class RefreshTokenRequest
    {
        /// <summary>Первичный ключ </summary>
        public Guid Id { get; set; }

        /// <summary>Токен для обновления AccessToken</summary>
        public required string RefreshToken { get; set; }
    }
}
