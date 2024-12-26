using AutoMapper;
using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.WebAPI.Models.Transaction;

namespace MoneyMaster.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности типы транзакций</summary>
    public class TransactionModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="TransactionModelMappingsProfile"/> </summary>
        public TransactionModelMappingsProfile()
        {
            CreateMap<TransactionDto, TransactionModel>();
        }
    }
}
