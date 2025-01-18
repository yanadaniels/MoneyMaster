namespace MoneyMaster.Domain.Entities
{
    public interface ISoftDeletable
    {
        bool IsDelete { get; set; }
    }
}
