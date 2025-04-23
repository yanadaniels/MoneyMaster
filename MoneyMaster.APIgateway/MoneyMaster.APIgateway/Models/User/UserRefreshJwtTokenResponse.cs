namespace MoneyMaster.APIgateway.Models.User
{
    /// <summary>
    /// Модель для возврата обновленного Access и Refresh токена
    /// </summary>
    public class UserRefreshJwtTokenResponse
    {
        /// <summary>Первичный ключ </summary>
        public Guid Id { get; set; }

        /// <summary>Токе доступа</summary>
        public required string AccsessToken { get; set; }

        /// <summary>Токе для обновления токеда доступа</summary>
        public required string RefreshToken { get; set; }
    }
}
