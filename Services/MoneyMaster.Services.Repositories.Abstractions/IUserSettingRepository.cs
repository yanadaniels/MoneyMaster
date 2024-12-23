using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с настройками пользователя
    /// </summary>
    public interface IUserSettingRepository : IRepository<UserSetting, Guid>
    {
    }
}
