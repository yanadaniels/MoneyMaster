using AutoMapper;
using MoneyMaster.Domain.Entities.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.TransactionType;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Services.Implementations
{
    /// <summary>Сервис работы с типами транзакций</summary>
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        /// <summary><inheritdoc cref="TransactionTypeService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="transactionTypeRepository">Репозиторий</param>
        public TransactionTypeService(IMapper mapper, ITransactionTypeRepository transactionTypeRepository)
        {
            _mapper = mapper;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<ICollection<TransactionTypeDto>> GetAllAsync()
        {
            ICollection<TransactionType> entities = _transactionTypeRepository.GetAll().ToList();
            return _mapper.Map<ICollection<TransactionType>, ICollection<TransactionTypeDto>>(entities);
        }

        public async Task<TransactionTypeDto> GetByIdAsync(Guid id)
        {
            var transactionType = await _transactionTypeRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<TransactionType, TransactionTypeDto>(transactionType);
        }
    }
}
