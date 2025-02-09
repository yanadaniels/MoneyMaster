using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.Services.Contracts.User;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterService.Services.Implementations
{
    /// <summary>Сервис работы с пользователем</summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        /// <summary><inheritdoc cref="UserService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="userRepository">Репозиторий</param>
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CreatingUserDto> AddAsync(CreatingUserDto item, CancellationToken Cancel = default)
        {
            if (item is null) return null;

            var user = _mapper.Map<CreatingUserDto, User>(item);

            if (await _userRepository.Exist(user, Cancel))
            {
                return null;
            }
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync(Cancel);

            return item;
        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            ICollection<User> entities =  _userRepository.GetAll().ToList();
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<User, UserDto>(user);
        }

        public Task<UserDto> GetByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
