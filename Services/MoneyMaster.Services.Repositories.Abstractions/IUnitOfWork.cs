namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// UOW.
    /// </summary>
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
