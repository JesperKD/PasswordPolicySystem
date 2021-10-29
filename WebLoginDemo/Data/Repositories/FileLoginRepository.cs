using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Data.Repositories
{
    public class FileLoginRepository : ILoginRepository
    {
        public Task<bool> CheckLogin(Login login)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Login createEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Login>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Login> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
