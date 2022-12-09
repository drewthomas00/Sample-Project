using VanderbiltFarms.Model;

namespace VanderbiltFarms.Service.Interfaces
{
    public interface ITransactionService
    {
        /// <summary>
        /// Get Transaction by Id
        /// </summary>
        public Task<Transaction> GetTransaction(int transactionId);

        /// <summary>
        /// Get all Transactions
        /// </summary>
        public Task<List<Transaction>> GetTransactions();

        /// <summary>
        /// Create a Transaction Record
        /// </summary>
        public Task CreateTransaction(Transaction transaction, int? feeId);

        /// <summary>
        /// Update Transaction by Id. Returns number of rows impacted.
        /// </summary>
        public Task<int> UpdateTransaction(Transaction transaction, int? feeId);
    }
}