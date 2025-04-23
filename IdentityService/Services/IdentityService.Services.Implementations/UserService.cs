using AutoMapper;
using IdentityService.Domain.Entities;
using IdentityService.Services.Abstractions;
using IdentityService.Services.Contracts.User;
using IdentityService.Services.Repositories.Abstractions;
using MoneyMaster.Common.Extensions;
using MoneyMaster.Common.Options;
using System.Security.Claims;

namespace IdentityService.Services.Implementations
{
    /// <summary>Сервис работы с пользователем</summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly AuthOptions _authOptions;

        /// <summary><inheritdoc cref="UserService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="userRepository">Репозиторий</param>
        public UserService(IMapper mapper, IUserRepository userRepository, AuthOptions authOptions)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _authOptions = authOptions;
        }

        public async Task<CreatingUserDto?> AddAsync(CreatingUserDto item, CancellationToken cancellationToken)
        {
            if (item is null) return null;

            var user = _mapper.Map<CreatingUserDto, User>(item);

            user.PasswordHash = UserHelper.GenerateHash(item.Password);

            if (await _userRepository.Exist(user, cancellationToken))
            {
                return null;
            }
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return item;
        }

        public async Task<UserJwtTokenDto?> SignIn(UserAuthorizeDto userAuthorize, CancellationToken cancellationToken)
        {
            if (userAuthorize == null) return null;

            var user = await _userRepository.AuthorizeUserAsync(userAuthorize.UserName, userAuthorize.Password, cancellationToken);
            if (user == null) return null;

            var userJwtToken = new UserJwtToken() { Id = user.Id, UserName = user.UserName, Email = user.Email, Role = user.Role };
            var identity = GetIdentity(userJwtToken);
            if (identity == null) return null;

            var newJwtToken = TokenProducer.GetJWTToken(identity.Claims, _authOptions);
            var newRefreshToken = TokenProducer.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userRepository.SaveChangesAsync(cancellationToken);

            var newUserJwtTokenDto = new UserJwtTokenDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                TelegramUserName = user.TelegramUserName,
                AccsessToken = newJwtToken,
                RefreshToken = newRefreshToken,
                CreateAt = user.CreateAt,
            };

            return newUserJwtTokenDto;
        }

        public async Task<bool> SignOut(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(id, cancellationToken);
            if (user == null) return false;

            user.RefreshToken = null;
            user.Expiration = null;

            await _userRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<UserRefreshJwtTokenDto?> RefreshToken(UserRefreshTokenDto userRefreshToken, CancellationToken cancellationToken)
        {
            if (userRefreshToken == null) return null;

            var user = await _userRepository.GetAsync(userRefreshToken.Id, cancellationToken);
            if (user == null || user.RefreshToken != userRefreshToken.RefreshToken) return null;

            if (user.Expiration < DateTime.UtcNow)
            {
                return null;
            }

            var userJwtToken = new UserJwtToken() { Id = user.Id, UserName = user.UserName, Email = user.Email, Role = user.Role };
            var identity = GetIdentity(userJwtToken);
            if (identity == null) return null;

            var newJwtToken = TokenProducer.GetJWTToken(identity.Claims, _authOptions);
            var newRefreshToken = TokenProducer.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.Expiration = DateTime.UtcNow.AddMinutes(_authOptions.RefreshTokenTime);

            await _userRepository.SaveChangesAsync(cancellationToken);

            var newUserJwtTokenDto = new UserRefreshJwtTokenDto()
            {
                Id = user.Id,
                AccsessToken = newJwtToken,
                RefreshToken = newRefreshToken,
            };

            return newUserJwtTokenDto;
        }

        private ClaimsIdentity GetIdentity(UserJwtToken person)
        {
            var claims = new List<Claim>
            {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role),
                    new Claim("ID", person.Id.ToString()),
                    new Claim("NameTelegram", person.TelegramUserName?.ToString()?? ""),
                    new Claim("Role", person.Role),
                    new Claim("EMail",person.Email)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public async Task<ICollection<UserDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            ICollection<User> entities = await _userRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(id, cancellationToken);
            if (user == null) return null;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto?> UpdateAsync(UserUpdateDto userDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(userDto.Id, cancellationToken);
            if (user == null) return null;

            _mapper.Map(userDto, user);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
    }
}
