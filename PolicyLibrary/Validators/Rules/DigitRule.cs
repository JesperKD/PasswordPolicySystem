using PolicyLibrary.Validators.Abstractions;
using System;
using System.Linq;

namespace PolicyLibrary.Validators.Rules
{
    public class DigitRule : IPolicyRule
    {
        private readonly byte _digitsRequired;

        /// <summary>
        /// Creates a digit rule.
        /// </summary>
        /// <param name="digitsRequired">The number of digits the rule must contain. Cannot exceed the maximum of <see cref="byte.MaxValue"/>. If 0, then the validator will just check for any numbers, otherwise will check for specific amount.</param>
        public DigitRule(byte digitsRequired = 0)
        {
            if (digitsRequired > byte.MaxValue)
            {
                throw new ArgumentException("Digits required exeeded capacity.", nameof(digitsRequired), null);
            }

            _digitsRequired = digitsRequired;
        }

        public void ValidatePolicy(object value, ref Validator validator)
        {
            if (value is string s)
            {
                if (_digitsRequired > 0)
                {
                    byte digitCounter = 0;

                    for (int i = 0; i < s.Length; i++)
                    {
                        char c = s[i];

                        if (char.IsDigit(c))
                        {
                            digitCounter++;
                        }
                    }

                    if (digitCounter < _digitsRequired)
                    {
                        string msg = $"{validator.Name} must have at least {_digitsRequired} digits.";
                        validator.AddException(
                            new ArgumentException(msg, validator.Name));
                    }

                    return;
                }

                if (!s.Any(c => char.IsDigit(c)))
                {
                    string msg = $"{validator.Name} must contain digits.";
                    validator.AddException(
                        new ArgumentException(msg, validator.Name));
                }
            }
        }
    }
}