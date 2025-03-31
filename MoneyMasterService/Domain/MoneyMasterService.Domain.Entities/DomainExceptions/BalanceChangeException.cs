namespace MoneyMasterService.Domain.Entities.DomainExceptions
{
    public class BalanceChangeException(Exception? innerException)
        : InternalServerException("Ошибка при изменении баланса", innerException);
}
