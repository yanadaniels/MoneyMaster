using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IUserSettingRepository"/></summary>
    public class UserSettingRepository : Repository<UserSetting, Guid>, IUserSettingRepository
    {
        /// <summary><inheritdoc cref="IUserSettingRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public UserSettingRepository(MoneyMasterContext context) : base(context) { }
    }
}
