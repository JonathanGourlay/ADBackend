using Serilog;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ADBackend.DAL.Repository
{
    public class BaseRepository
    {
        private readonly string _sqlconnectionString;

        protected BaseRepository(ConnectionStrings connectionStrings)
        {
            _sqlconnectionString = connectionStrings.SQLServer;
        }

        protected T ExecuteFunc<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using var connection = new SqlConnection(_sqlconnectionString);
                connection.Open();
                return  getData(connection);
            }
            catch (Exception ex)
            {
                var exceptionMsg = $"{GetType().FullName}.ExecuteFunc experienced a {ex.GetType()}";
                Log.Error(ex, exceptionMsg);
                throw new Exception(exceptionMsg, ex);
            }
        }
    }
}

