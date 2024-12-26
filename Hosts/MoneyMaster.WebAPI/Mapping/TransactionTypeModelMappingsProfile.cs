using AutoMapper;
using MoneyMaster.Services.Contracts.TransactionType;
using MoneyMaster.WebAPI.Models.TransactionType;

namespace MoneyMaster.WebAPI.Mapping
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
