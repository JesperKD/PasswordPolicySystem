using PolicyLibrary.Validators.Abstractions;
using System;

namespace PolicyLibrary.Validators.Rules
{
    public class MinimumLengthRule : IPolicyRule
    {
        private readonly byte _minLength;

        /// <summary>
        /// Creates a minimum length rule.
        /// </summary>
        /// <param name="minimumLength">Specifies the minimum length of the rule. Cannot be higher than <see cref="byte.MaxValue"/>.</param>
        public MinimumLengthRule(byte minimumLength)
        {
            if (minimumLength > byte.MaxValue)
            {
                throw new ArgumentException("Minimum Length exeeded capacity.", nameof(minimumLength), null);
            }

            _minLength = minimumLength;
        }

        public void ValidatePolicy(object value, ref Validator validator)
        {
            if (value is string s)
            {
                if (s.Length < _minLength)
                {
                    string msg = $"{validator.Name} must be at least {_minLength}.";
                    validator.AddException(
                        new ArgumentException(msg, validator.Name));
                }
            }
        }
    }
}
