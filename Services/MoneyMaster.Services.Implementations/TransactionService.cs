﻿using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Services.Implementations
{
    /// <summary>Сервис работы с транзакциями</summary>
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        /// <summary><inheritdoc cref="TransactionService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="transactionRepository">Репозиторий</param>
        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<ICollection<TransactionDto>> GetAllAsync()
        {
            ICollection<Transaction> entities = _transactionRepository.GetAll().ToList();
            return _mapper.Map<ICollection<Transaction>, ICollection<TransactionDto>>(entities);
        }

        public async Task<TransactionDto> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Transaction, TransactionDto>(transaction);
        }
    }
}
