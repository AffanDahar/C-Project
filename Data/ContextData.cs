using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextDapper
    {
        private readonly string _connectionString;

        public DataContextDapper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
        }

        public IEnumerable<T> GetData<T>(string sqlQuery)
        {
            IDbConnection databaseConnection = new SqlConnection(_connectionString);
            return databaseConnection.Query<T>(sqlQuery);
            
        }
        public T GetSingleData<T>(string sqlQuery)
        {
            IDbConnection databaseConnection = new SqlConnection(_connectionString);
            return databaseConnection.QuerySingle<T>(sqlQuery);
            
        }
        public int ExecuteQueryRow(string sqlQuery)
        {
            IDbConnection databaseConnection = new SqlConnection(_connectionString);
            return databaseConnection.Execute(sqlQuery);
        }
        public bool ExecuteQuery(string sqlQuery)
        {
            IDbConnection databaseConnection = new SqlConnection(_connectionString);
            return databaseConnection.Execute(sqlQuery) > 0;
        }
    }
}