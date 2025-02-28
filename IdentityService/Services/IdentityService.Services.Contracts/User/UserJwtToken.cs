// Ignore Spelling: Jwt Dto

namespace IdentityService.Services.Contracts.User
{
    public class UserJwtToken
    {
        public Guid Id { get; set; }

        public required string UserName { get; set; }

        public required string Role { get; set; }

        public string? TelegramUserName { get; set; }

        public required string Email { get; set; }
    }
}
