using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;    
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebLoginDemo.Data
{
    public class SqlDatabase : Database
    {
        private readonly SqlConnection _sqlConnection;

        public SqlDatabase(IConfiguration configuration) : base(configuration)
        {
            string connString = "Server=(localdb)\\MSSQLLOCALDB; Database=PasswordPolicyDB;";
            _sqlConnection = new(connString);
        }

        /// <summary>
        /// Opens database connection
        /// </summary>
        /// <returns></returns>
        public override async Task OpenConnectionAsync()
        {
            if (_sqlConnection.State == ConnectionState.Open) return;

            await _sqlConnection.OpenAsync();
        }

        /// <summary>
        /// Closes database connection
        /// </summary>
        /// <returns></returns>
        public override async Task CloseConnectionAsync()
        {
            if (_sqlConnection.State == ConnectionState.Closed) return;

            await _sqlConnection.CloseAsync();
        }

        /// <summary>
        /// Executes database query
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public override async Task ExecuteNonQueryAsync(string cmdText = null, IDictionary<string, object> sqlParams = null)
        {
            using SqlCommand commandObj = new()
            {
                CommandText = cmdText,
                CommandType = CommandType.Text,
                Connection = _sqlConnection
            };

            if (SqlParamsIsNotNull(sqlParams))
            {
                AddSqlParamsToSqlCommand(sqlParams, commandObj);
            }

            await OpenConnectionAsync();

            await commandObj.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Returns reader data from database
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public override async Task<DbDataReader> GetDataReaderAsync(string cmdText = null, IDictionary<string, object> sqlParams = null)
        {
            using SqlCommand commandObj = new()
            {
                CommandText = cmdText,
                CommandType = CommandType.Text,
                Connection = _sqlConnection
            };

            if (SqlParamsIsNotNull(sqlParams))
            {
                AddSqlParamsToSqlCommand(sqlParams, commandObj);
            }

            await OpenConnectionAsync();

            return await commandObj.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Checks if the sqlParam dictionary has any values
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private static bool SqlParamsIsNotNull(IDictionary<string, object> sqlParams)
        {
            return sqlParams != null;
        }

        /// <summary>
        /// Adds sql parameters to sql command
        /// </summary>
        /// <param name="sqlParams"></param>
        /// <param name="commandObj"></param>
        private static void AddSqlParamsToSqlCommand(IDictionary<string, object> sqlParams, SqlCommand commandObj)
        {
            foreach (var param in sqlParams)
            {
                commandObj.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
    }
}
