namespace MoneyMasterService.Domain.Entities.DomainExceptions
{
    public class InternalServerException(string message, Exception? innerException = null)
        : Exception(message, innerException);
}
