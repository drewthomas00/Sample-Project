using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;

namespace VanderbiltFarms.DataAccess
{
    public class PlotRepository : IPlotRepository
    {
        private readonly ILogger<PlotRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;

        public PlotRepository(ILogger<PlotRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task CreatePlot(Plot plot, int? homeownerId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" INSERT INTO plots (homeowner_id, street_number, street_name, acres, square_feet, bedrooms, bathrooms, description)
                            VALUES
                            (
                                {(homeownerId == null ? "NULL" : homeownerId)},
                                '{plot.StreetNumber}',
                                '{plot.StreetName}',
                                '{plot.Acres}',
                                '{plot.SquareFeet}',
                                '{plot.Bedrooms}',
                                '{plot.Bathrooms}',
                                '{plot.Description}'
                            );"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating Plot");
                throw;
            }
        }

        public async Task DeletePlot(int plotId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    await connection.ExecuteAsync(
                        @$" DELETE FROM plots
                            WHERE plot_id = {plotId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting Plot");
                throw;
            }
        }

        public async Task<Plot> GetPlot(int plotId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.QuerySingleAsync<Plot>(
                        @$" SELECT *
                            FROM plots
                            WHERE plot_id = {plotId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Plot by Id");
                throw;
            }
        }

        public async Task<List<Plot>> GetPlots()
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Plot>(
                        @"  SELECT *
                            FROM plots;"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Plots");
                throw;
            }
        }

        public async Task<List<Fee>> GetPlotsForHomeowner(int homeownerId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return (await connection.QueryAsync<Fee>(
                        @$" SELECT *
                            FROM plots
                            WHERE homeowner_id = {homeownerId};"
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Plots for a Homeowner");
                throw;
            }
        }

        public async Task<int> UpdatePlot(Plot plot, int? homeownerId)
        {
            try
            {
                using (NpgsqlConnection connection = _databaseConnection.Create())
                {
                    return await connection.ExecuteAsync(
                        @$" UPDATE plots
                            SET (homeowner_id, street_number, street_name, acres, square_feet, bedrooms, bathrooms, description) =
                            (
                                {(homeownerId == null ? "NULL" : homeownerId)},
                                '{plot.StreetNumber}',
                                '{plot.StreetName}',
                                '{plot.Acres}',
                                '{plot.SquareFeet}',
                                '{plot.Bedrooms}',
                                '{plot.Bathrooms}',
                                '{plot.Description}'
                            )
                            WHERE plot_id = {plot.PlotId};"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Plot");
                throw;
            }
        }
    }
}