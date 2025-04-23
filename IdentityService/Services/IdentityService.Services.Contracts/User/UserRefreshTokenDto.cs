namespace IdentityService.Services.Contracts.User
{
    /// <summary>
    /// DTO для обновления Access и Refresh токена
    /// </summary>
    public record UserRefreshTokenDto
    {
        /// <summary>Первичный ключ </summary>
        public Guid Id { get; set; }

        /// <summary>Токен для обновления AccessToken</summary>
        public required string RefreshToken { get; set; }
    }
}
