using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Transaction;

namespace MoneyMasterService.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности транзакций</summary>
    public class TransactionMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="TransactionMappingsProfile"/> </summary>
        public TransactionMappingsProfile()
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
