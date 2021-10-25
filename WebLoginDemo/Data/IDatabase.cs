using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace WebLoginDemo.Data
{
    public interface IDatabase
    {
        Task<DbDataReader> GetDataReaderAsync(string cmdText = null, IDictionary<string, object> sqlParams = null);

        Task ExecuteNonQueryAsync(string cmdText = null, IDictionary<string, object> sqlParams = null);
    }
}
