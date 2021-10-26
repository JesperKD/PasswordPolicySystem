using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLoginDemo.Data.Repositories;
using WebLoginDemo.DataModels;

namespace WebLoginDemo.Data.Services
{
    public class LoginService
    {
        private readonly LoginRepository _loginRepository;

        public LoginService(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

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

        public async Task<IEnumerable<Login>> GetAllAsync()
        {
            return await _loginRepository.GetAllAsync();
        }

        public async Task<Login> GetByUsernameAsync(string username)
        {
            return await _loginRepository.GetByUsernameAsync(username);
        }


    }
}
