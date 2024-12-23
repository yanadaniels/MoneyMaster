using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.User;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Services.Implementations
{
    /// <summary>Сервис работы с пользователем</summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            ICollection<User> entities = _userRepository.GetAll().ToList();
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        public Task<UserDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByIdAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
