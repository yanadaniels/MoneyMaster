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
        Task<UserDto> GetByIdAsync(string userName);

        /// <summary>
        /// Получить список пользователей.
        /// </summary>
        /// <returns> Список DTO пользователей. </returns>
        Task<ICollection<UserDto>> GetAllAsync();


    }
}
