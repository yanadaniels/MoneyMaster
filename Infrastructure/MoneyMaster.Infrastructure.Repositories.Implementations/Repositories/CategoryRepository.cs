using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="ICategoryRepository"/></summary>
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        /// <summary><inheritdoc cref="ICategoryRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public CategoryRepository(MoneyMasterContext context) : base(context) { }
    }
}
