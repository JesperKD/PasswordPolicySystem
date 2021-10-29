using System;

namespace WebLoginDemo.Data.Services
{
    public interface IValidationService
    {
        /// <summary>
        /// Attempts to validate a given password.
        /// </summary>
        /// <param name="password"></param>
        /// <exception cref="ArgumentException">Is thrown if the password couldn't be validated.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if no policy is configured in the configuration.</exception>
        void ValidatePassword(string password);
    }
}
