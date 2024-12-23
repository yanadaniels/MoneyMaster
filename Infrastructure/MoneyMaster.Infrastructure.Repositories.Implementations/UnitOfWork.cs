using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// UOW.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private MoneyMasterContext _context;

        public UnitOfWork(MoneyMasterContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
