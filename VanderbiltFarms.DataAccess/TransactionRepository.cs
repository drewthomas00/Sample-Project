using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.DataAccess
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ILogger<TransactionRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;

        public TransactionRepository(ILogger<TransactionRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task CreateTransaction(Transaction transaction, int? feeId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" INSERT INTO transactions (fee_id, amount, transaction_date, description)
                            VALUES
                            (
                                {(feeId == null ? "NULL" : feeId)},
                                '{transaction.Amount}',
                                '{transaction.TransactionDate}',
                                '{transaction.Description}'
                            );"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating Transaction");
                throw;
            }
        }

        public async Task<Transaction> GetTransaction(int transactionId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.QuerySingleAsync<Transaction>(
                        @$" SELECT *
                            FROM transactions
                            WHERE transaction_id = {transactionId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Transaction by Id");
                throw;
            }
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Transaction>(
                        @"  SELECT *
                            FROM transactions;"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Transactions");
                throw;
            }
        }

        public async Task<List<Transaction>> GetTransactionsForFee(int feeId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Transaction>(
                        @$" SELECT *
                            FROM transactions
                            WHERE fee_id = {feeId};"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Transactions for a Fee");
                throw;
            }
        }

        public async Task<int> UpdateTransaction(Transaction transaction, int? feeId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.ExecuteAsync(
                        @$" UPDATE transactions
                            SET (fee_id, amount, transaction_date, description) =
                            (
                                {(feeId == null ? "NULL" : feeId)},
                                '{transaction.Amount}',
                                '{transaction.TransactionDate}',
                                '{transaction.Description}'
                            )
                            WHERE transaction_id = {transaction.TransactionID};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Transaction");
                throw;
            }
        }
    }
}