using Npgsql;

namespace VanderbiltFarms.DataAccess.Helpers
{
    public interface IDatabaseConnection
    {
        NpgsqlConnection Create();
    }

    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection Create()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}