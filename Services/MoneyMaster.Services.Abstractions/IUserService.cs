using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с пользователем</summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="userName">UserName пользователя . </param>
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto> GetByUserNameAsync(string userName);

        /// <summary>
        /// Получить список пользователей.
        /// </summary>
        /// <returns> Список DTO пользователей. </returns>
        Task<ICollection<UserDto>> GetAllAsync();

        /// <summary>Добавление пользователя</summary>
        /// <param name="item">Добавляемая сущность пользователя</param>
        /// <param name="Cancel">Признак отмены асинхронной операции</param>
        /// <returns>Добавленная в репозиторий сущность</returns>
        Task<CreatingUserDto> AddAsync(CreatingUserDto item, CancellationToken Cancel = default);


    }
}
