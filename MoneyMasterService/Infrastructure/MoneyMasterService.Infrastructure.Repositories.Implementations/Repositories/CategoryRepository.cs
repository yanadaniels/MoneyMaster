using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="ICategoryRepository"/></summary>
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        /// <summary><inheritdoc cref="ICategoryRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public CategoryRepository(MoneyMasterServiceContext context) : base(context) { }
    }
}
