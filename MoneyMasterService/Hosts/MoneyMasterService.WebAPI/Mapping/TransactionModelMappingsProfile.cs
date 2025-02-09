using AutoMapper;
using MoneyMasterService.Services.Contracts.Transaction;
using MoneyMasterService.WebAPI.Models.Transaction;

namespace MoneyMasterService.WebAPI.Mapping
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
