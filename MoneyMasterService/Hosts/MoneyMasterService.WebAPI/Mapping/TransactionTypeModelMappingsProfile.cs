using AutoMapper;
using MoneyMasterService.Services.Contracts.TransactionType;
using MoneyMasterService.WebAPI.Models.TransactionType;

namespace MoneyMasterService.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности типы транзакций</summary>
    public class TransactionTypeModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="TransactionTypeModelMappingsProfile"/> </summary>
        public TransactionTypeModelMappingsProfile()
        {
            CreateMap<TransactionTypeDto, TransactionTypeModel>();
        }
    }
}
