using AutoMapper;
using MoneyMaster.Domain.Entities.Entities;
using MoneyMaster.Services.Contracts.TransactionType;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности типы транзакций</summary>
    public class TransactionTypeMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="TransactionTypeMappingsProfile"/> </summary>
        public TransactionTypeMappingsProfile()
        {
            CreateMap<TransactionType, TransactionTypeDto>();
        }
    }
}
