namespace MoneyMaster.Domain.Entities.DomainExceptions;

public class NotFoundException(string message, Exception? innerException = null) : Exception(message, innerException);