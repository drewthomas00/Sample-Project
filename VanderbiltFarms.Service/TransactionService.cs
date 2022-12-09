using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;

namespace VanderbiltFarms.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task CreateTransaction(Transaction transaction, int? feeId)
        {
            await _transactionRepository.CreateTransaction(transaction, feeId);
        }

        public async Task<Transaction> GetTransaction(int transactionId)
        {
            return await _transactionRepository.GetTransaction(transactionId);
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetTransactions();
        }

        public Task<int> UpdateTransaction(Transaction transaction, int? feeId)
        {
            return _transactionRepository.UpdateTransaction(transaction, feeId);
        }
    }
}