using Microsoft.Extensions.Configuration;
using PolicyLibrary.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLoginDemo.Data.DataModels;

namespace WebLoginDemo.Data.Services
{
    public class DefaultValidationService : IValidationService
    {
        private readonly PolicySettings _policySettings;
        private readonly DefaultValidator _validator;

        public DefaultValidationService(PolicySettings policySettings, DefaultValidator validator)
        {
            _policySettings = policySettings;
            _validator = validator;
        }

        public void ValidatePassword(string password)
        {
            switch (_policySettings.Policy)
            {
                case Enums.Policy.PartOne:
                    _validator.ValidatePolicyPartOneException(password);
                    break;
                case Enums.Policy.PartTwo:
                    _validator.ValidatePolicyPartTwoException(password);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("No policy was configured.", nameof(_policySettings));
            }
        }
    }
}
