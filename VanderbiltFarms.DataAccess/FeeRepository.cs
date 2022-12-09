using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.DataAccess
{
    public class FeeRepository : IFeeRepository
    {
        private readonly ILogger<FeeRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;

        public FeeRepository(ILogger<FeeRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task CreateFee(Fee fee, int plotId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" INSERT INTO fees (plot_id, amount, due_date, description)
                            VALUES ('{plotId}', '{fee.Amount}', '{fee.DueDate}', '{fee.Description}');"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating Fee");
                throw;
            }
        }

        public async Task DeleteFee(int feeId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" DELETE FROM fees
                            WHERE fee_id = {feeId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting Fee");
                throw;
            }
        }

        public async Task<Fee> GetFee(int feeId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.QuerySingleAsync<Fee>(
                        @$" SELECT *
                            FROM fees
                            WHERE fee_id = {feeId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Fee by Id");
                throw;
            }
        }

        public async Task<List<Fee>> GetFees()
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Fee>(
                        @"  SELECT *
                            FROM fees;"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Fees");
                throw;
            }
        }

        public async Task<List<Fee>> GetFeesForPlot(int plotId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Fee>(
                        @$" SELECT *
                            FROM fees
                            WHERE plot_id = {plotId};"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Fees for a Plot");
                throw;
            }
        }

        public async Task<int> UpdateFee(Fee fee, int plotId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.ExecuteAsync(
                        @$" UPDATE fees
                            SET plot_id = '{plotId}', amount = '{fee.Amount}', due_date = '{fee.DueDate}', description = '{fee.Description}'
                            WHERE fee_id = {fee.FeeId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Fee");
                throw;
            }
        }
    }
}