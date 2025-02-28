namespace MoneyMasterService.Domain.Entities.DomainExceptions
{
    public class TransactionNotFoundException(Exception? innerException = null)
        : NotFoundException("Не удалось найти транзакцию", innerException);
}
