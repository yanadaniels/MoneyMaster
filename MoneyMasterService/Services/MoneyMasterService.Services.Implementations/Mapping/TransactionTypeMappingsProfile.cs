using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.TransactionType;

namespace MoneyMasterService.Services.Implementations.Mapping
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
