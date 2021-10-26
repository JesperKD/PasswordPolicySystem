using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Data.Repositories
{
    public class LoginRepository
    {
        private readonly IDatabase _sqlDatabase;

        public LoginRepository(IDatabase database)
        {
            _sqlDatabase = database;
        }

        /// <summary>
        /// Saves a new login to the database
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns></returns>
        public async Task CreateAsync(Login createEntity)
        {
            string cmdText = @"INSERT INTO `PasswordPolicyDB`.`Login`
                                (`username`, `password`, `attempts`)
                                VALUES (@username, @password, @attempts)";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@username", createEntity.Username},
                { "@password", createEntity.Password},
                { "@attempts", createEntity.Attempts}
            };

            await _sqlDatabase.ExecuteNonQueryAsync(cmdText, sqlParams);
        }

        /// <summary>
        /// Returns all login data from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Login>> GetAllAsync()
        {
            string cmdText = @"SELECT * FROM `PasswordPolicyDB`.`Login`";

            using var datareader = await _sqlDatabase.GetDataReaderAsync(cmdText);

            if (datareader.HasRows == false) return Enumerable.Empty<Login>();

            List<Login> logins = new();

            while(await datareader.ReadAsync())
            {
                Login tempLogin = new(
                    username: datareader.GetString(1),
                    password: datareader.GetString(2),
                    attempts: datareader.GetInt32(3)
                    );
                logins.Add(tempLogin);
            }

            return logins;
        }

        /// <summary>
        /// Returns a specific login from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Login> GetByUsernameAsync(string username)
        {
            string cmdText = @"SELECT * FROM `PasswordPolicyDB`.`Login` WHERE `username` = @username";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                { "@username", username}
            };

            var dataReader = await _sqlDatabase.GetDataReaderAsync(cmdText);

            if (dataReader.HasRows == false) return null;

            Login login = null;

            while (await dataReader.ReadAsync())
            {
                login = new(
                    username: dataReader.GetString(1),
                    password: dataReader.GetString(2),
                    attempts: dataReader.GetInt32(3)
                    );
            }

            return login;
        }

    }
}
