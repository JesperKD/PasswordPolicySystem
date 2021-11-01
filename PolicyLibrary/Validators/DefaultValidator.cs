using PolicyLibrary.Validators.Abstractions;
using PolicyLibrary.Validators.Rules;
using System;
using System.Collections.Generic;

namespace PolicyLibrary.Validators
{
    public class DefaultValidator
    {
        private List<IPolicyRule> _policyRulesPartOne = new()
        {
            new MinimumLengthRule(12),
            new CapitalLetterRule(),
            new LowercaseLetterRule(),
            new SpecialCharacterRule(),
            new DigitRule()
        };

        private List<IPolicyRule> _policyRulesPartTwo = new()
        {
            new MinimumLengthRule(18),
            new CapitalLetterRule(2),
            new SpecialCharacterRule(1),
            new DigitRule(3)
        };

        public void ValidatePolicyPartOneException(string password)
        {
            Validator v = new("PasswordPolicyPartOne", _policyRulesPartOne);

            if (!v.Validate(password))
            {
                foreach (Exception exception in v.GetExceptions)
                {
                    throw exception;
                }
            }
        }

        public void ValidatePolicyPartTwoException(string password)
        {
            Validator v = new("PasswordPolicyPartTwo", _policyRulesPartTwo);

            if (!v.Validate(password))
            {
                foreach (Exception exception in v.GetExceptions)
                {
                    throw exception;
                }
            }
        }
    }
}
