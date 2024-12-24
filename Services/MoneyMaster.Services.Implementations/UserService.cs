﻿using AutoMapper;
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

        /// <summary><inheritdoc cref="UserService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="userRepository">Репозиторий</param>
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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
