using PolicyLibrary.Validators.Abstractions;
using System;
using System.Linq;

namespace PolicyLibrary.Validators.Rules
{
    public class SpecialCharacterRule : IPolicyRule
    {
        private readonly byte _specialCharactersRequired;

        public SpecialCharacterRule(byte specialCharactersRequired = 0)
        {
            if (specialCharactersRequired > byte.MaxValue)
            {
                throw new ArgumentException("Required special characters exceeds capacity.", nameof(specialCharactersRequired));
            }

            _specialCharactersRequired = specialCharactersRequired;
        }

        public void ValidatePolicy(object value, ref Validator validator)
        {
            if (value is string s)
            {
                if (_specialCharactersRequired > 0)
                {
                    byte specialCharactersCounter = 0;

                    for (int i = 0; i < s.Length; i++)
                    {
                        char c = s[i];

                        if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                        {
                            specialCharactersCounter++;
                        }
                    }

                    if (specialCharactersCounter < _specialCharactersRequired)
                    {
                        string msg = $"{validator.Name} must have at least {_specialCharactersRequired} special characters.";
                        validator.AddException(
                            new ArgumentException(msg, validator.Name));
                    }

                    return;
                }               

                string withoutSpecial = new(s.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c) || char.IsDigit(c)).ToArray());
                if (s.Equals(withoutSpecial))
                {
                    string msg = $"{validator.Name} must contain a special character and a digit(s).";
                    validator.AddException(
                        new ArgumentException(msg, validator.Name));
                }
            }
        }
    }
}
