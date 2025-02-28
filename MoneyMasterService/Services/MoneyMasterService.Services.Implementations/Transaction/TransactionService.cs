using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Domain.Entities.DomainExceptions;
using MoneyMasterService.Services.Abstractions.Transaction;
using MoneyMasterService.Services.Contracts.Transaction;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterService.Services.Implementations
{
    /// <summary>Сервис работы с транзакциями</summary>
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBalanceChanger _balanceChanger;

        /// <summary><inheritdoc cref="TransactionService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="transactionRepository">Репозиторий</param>
        /// <param name="balanceChanger">Вспомогательный класс для изменения баланса</param>
        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository,
            IBalanceChanger balanceChanger)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _balanceChanger = balanceChanger;
        }

        public async Task<TransactionResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetAsync(id, cancellationToken);
            return transaction is null
                ? null
                : _mapper.Map<Transaction, TransactionResponse>(transaction);
        }

        public async Task<IReadOnlyCollection<TransactionResponse?>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _transactionRepository.GetAllAsync(cancellationToken);
            return _mapper
                .Map<IReadOnlyCollection<Transaction>,
                    IReadOnlyCollection<TransactionResponse>>(entities);
        }

        public async Task<Guid> CreateAsync(CreatingTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = TransactionFactory.Create(request);

            await _transactionRepository.AddAsync(transaction, cancellationToken);

            await _balanceChanger.ChangeBalanceWithRollbackAsync(
                transaction, () =>
                {
                    _transactionRepository.Delete(transaction);
                    return _transactionRepository.SaveChangesAsync(cancellationToken);
                },
                false,
                cancellationToken
            );

            await _transactionRepository.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }

        public async Task<TransactionResponse> UpdateAsync(Guid id, UpdatingTransactionRequest request,
            CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetAsync(id, cancellationToken);

            if (transaction is null)
            {
                throw new TransactionNotFoundException();
            }

            await _balanceChanger.ChangeBalanceWithRollbackAsync(transaction, () => Task.CompletedTask, true,
                cancellationToken);

            transaction.AccountId = request.AccountId;
            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Description = request.Description;


            _transactionRepository.Update(transaction);

            await _balanceChanger.ChangeBalanceWithRollbackAsync(transaction, () =>
            {
                _transactionRepository.Update(transaction);
                return _transactionRepository.SaveChangesAsync(cancellationToken);
            },
                false,
                cancellationToken
            );

            await _transactionRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Transaction, TransactionResponse>(transaction);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var deletedTransaction = await _transactionRepository.SoftDeleteAsync(id, cancellationToken);

            if (deletedTransaction is null)
            {
                throw new TransactionNotFoundException();
            }

            await _balanceChanger.ChangeBalanceWithRollbackAsync(deletedTransaction, () =>
            {
                _transactionRepository.RestoreAsync(id, cancellationToken);
                return _transactionRepository.SaveChangesAsync(cancellationToken);
            },
                true,
                cancellationToken
            );

            await _transactionRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<TransactionResponse> RestoreAsync(Guid id, CancellationToken cancellationToken)
        {
            var restoredTransaction = await _transactionRepository.RestoreAsync(id, cancellationToken);

            if (restoredTransaction is null)
            {
                throw new TransactionNotFoundException();
            }

            await _balanceChanger.ChangeBalanceWithRollbackAsync(restoredTransaction, () =>
            {
                _transactionRepository.SoftDeleteAsync(id, cancellationToken);
                return _transactionRepository.SaveChangesAsync(cancellationToken);
            },
                false,
                cancellationToken
            );

            await _transactionRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Transaction, TransactionResponse>(restoredTransaction);
        }
    }
}