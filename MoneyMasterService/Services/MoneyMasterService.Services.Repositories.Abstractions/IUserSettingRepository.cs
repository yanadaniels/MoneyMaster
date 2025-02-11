using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с настройками пользователя
    /// </summary>
    public interface IUserSettingRepository : IRepository<UserSetting, Guid>
    {
    }
}
