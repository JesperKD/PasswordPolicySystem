using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLoginDemo.Data.Repositories;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Data.Services
{
    public class LoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// Saves a new login in the database
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(Login createEntity)
        {
            try
            {
                await _loginRepository.CreateAsync(createEntity);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Returns all logins from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Login>> GetAllAsync()
        {
            return await _loginRepository.GetAllAsync();
        }

        /// <summary>
        /// Returns a specific login from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Login> GetByUsernameAsync(string username)
        {
            return await _loginRepository.GetByUsernameAsync(username);
        }

        /// <summary>
        /// Tries to match given password with saved one
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<bool> CheckLogin(Login login)
        {
            return await _loginRepository.CheckLogin(login);
        }


    }
}
