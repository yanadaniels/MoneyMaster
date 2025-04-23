using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IUserSettingRepository"/></summary>
    public class UserSettingRepository : Repository<UserSetting, Guid>, IUserSettingRepository
    {
        /// <summary><inheritdoc cref="IUserSettingRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public UserSettingRepository(MoneyMasterServiceContext context) : base(context) { }
    }
}
