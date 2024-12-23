using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с типом учетной записи.
    /// </summary>
    public interface IAccountTypeRepository : IRepository<AccountType, Guid>
    {
    }
}
