using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Data.Repositories
{
    public interface ILoginRepository
    {
        Task CreateAsync(Login createEntity);
        Task<IEnumerable<Login>> GetAllAsync();
        Task<Login> GetByUsernameAsync(string username);
        Task<bool> CheckLogin(Login login);
    }
}
