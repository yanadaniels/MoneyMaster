using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.Transaction;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности транзакций</summary>
    public class TransactionMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="TransactionMappingsProfile"/> </summary>
        public TransactionMappingsProfile()
        {
            CreateMap<Domain.Entities.Entities.Transaction, TransactionResponse>();
        }
    }
}
