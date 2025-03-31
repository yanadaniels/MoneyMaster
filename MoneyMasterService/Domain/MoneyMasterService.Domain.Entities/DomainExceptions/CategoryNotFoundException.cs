namespace MoneyMasterService.Domain.Entities.DomainExceptions
{
    public class CategoryNotFoundException(Exception? innerException = null)
        : NotFoundException("Не удалось найти категорию", innerException);
}
