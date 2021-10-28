using System;
using System.Linq;
using WebLoginDemo.Data.Validators.Abstractions;

namespace WebLoginDemo.Data.Validators.Rules
{
    public class CapitalLetterRule : IPolicyRule
    {
        private readonly byte _requiredCapitalLetters;

        public CapitalLetterRule(byte requiredCapitalLetters = 0)
        {
            if (requiredCapitalLetters > byte.MaxValue)
            {
                throw new ArgumentException("Capital letters required exeeds capacity.", nameof(requiredCapitalLetters));
            }

            _requiredCapitalLetters = requiredCapitalLetters;
        }

        public void ValidatePolicy(object value, ref Validator validator)
        {
            if (value is string s)
            {
                if (_requiredCapitalLetters > 0)
                {
                    byte capitalLetterCounter = 0;

                    for (int i = 0; i < s.Length; i++)
                    {
                        char c = s[i];

                        if (char.IsUpper(c))
                        {
                            capitalLetterCounter++;
                        }
                    }

                    if (capitalLetterCounter < _requiredCapitalLetters)
                    {
                        string msg = $"{validator.Name} must have at least {_requiredCapitalLetters} capital letters.";
                        validator.AddException(
                            new ArgumentException(msg, validator.Name));
                    }

                    return;
                }

                if (!s.Any(char.IsUpper))
                {
                    string msg = $"{validator.Name} must contain at least one capital letter.";
                    validator.AddException(
                        new ArgumentException(msg, validator.Name));
                }
            }
        }
    }
}
