namespace MoneyMaster.Domain.Entities.DomainExceptions;

public class TransactionNotFoundException(Exception? innerException = null)
    : NotFoundException("Не удалось найти транзакцию", innerException);