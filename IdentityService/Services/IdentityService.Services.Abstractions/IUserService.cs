using IdentityService.Services.Contracts.User;

namespace IdentityService.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с пользователем</summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="userName">UserName пользователя . </param>
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список пользователей.
        /// </summary>
        /// <returns> Список DTO пользователей. </returns>
        Task<ICollection<UserDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>Добавление пользователя</summary>
        /// <param name="item">Добавляемая сущность пользователя</param>
        /// <param name="Cancel">Признак отмены асинхронной операции</param>
        /// <returns>Добавленная в репозиторий сущность</returns>
        Task<CreatingUserDto?> AddAsync(CreatingUserDto item, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизация пользователя по имени и паролю
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        public Task<UserJwtTokenDto?> SignIn(UserAuthorizeDto user, CancellationToken cancellationToken);

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        public Task<bool> SignOut(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить refreshToken
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <returns>RefreshToken</returns>
        public Task<UserRefreshJwtTokenDto?> RefreshToken(UserRefreshTokenDto user, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="updatedUser">Сущность пользователя с обновленными данными</param>
        /// <returns></returns>
        public Task<UserDto?> UpdateAsync(UserUpdateDto updatedUser, CancellationToken cancellationToken);
    }
}
