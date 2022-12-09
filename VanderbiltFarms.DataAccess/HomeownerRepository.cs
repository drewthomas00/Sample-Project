using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.DataAccess
{
    public class HomeownerRepository : IHomeownerRepository
    {
        private readonly ILogger<HomeownerRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;

        public HomeownerRepository(ILogger<HomeownerRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task CreateHomeowner(Homeowner homeowner)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" INSERT INTO homeowners (full_name, phone, email, birthday, description)
                            VALUES ('{homeowner.FullName}', '{homeowner.Phone}', '{homeowner.Email}', '{homeowner.Birthday}', '{homeowner.Description}');"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating Homeowner");
                throw;
            }
        }

        public async Task DeleteHomeowner(int homeownerId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" UPDATE plots
                            SET homeowner_id = NULL
                            WHERE homeowner_id = {homeownerId};"
                    );

                    await connection.ExecuteAsync(
                        @$" DELETE FROM homeowners
                            WHERE homeowner_id = {homeownerId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting Homeowner");
                throw;
            }
        }

        public async Task<Homeowner> GetHomeowner(int homeownerId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.QuerySingleAsync<Homeowner>(
                        @$" SELECT *
                            FROM homeowners
                            WHERE homeowner_id = {homeownerId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Homeowner by Id");
                throw;
            }
        }

        public async Task<List<Homeowner>> GetHomeowners()
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Homeowner>(
                        @"  SELECT *
                            FROM homeowners;"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Homeowners");
                throw;
            }
        }

        public async Task<List<Homeowner>> SearchHomeowner(string searchTerm)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Homeowner>(
                        @$" SELECT *
                            FROM homeowners
                            WHERE LOWER(full_name) LIKE LOWER('%{searchTerm}%');"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Searching for Homeowners");
                throw;
            }
        }

        public async Task<int> UpdateHomeowner(Homeowner homeowner)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.ExecuteAsync(
                        @$" UPDATE homeowners
                            SET full_name = '{homeowner.FullName}', phone = '{homeowner.Phone}', email = '{homeowner.Email}', description = '{homeowner.Description}', birthday = '{homeowner.Birthday}'
                            WHERE homeowner_id = {homeowner.HomeownerId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Homeowner");
                throw;
            }
        }
    }
}