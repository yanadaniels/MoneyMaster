namespace MoneyMasterService.Domain.Entities.DomainExceptions
{
    public class AccountNotFoundException(Exception? exception = null)
        : NotFoundException("Не удалось найти акаунт", exception);
}
