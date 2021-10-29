using PolicyLibrary.Validators.Abstractions;
using System;
using System.Linq;

namespace PolicyLibrary.Validators.Rules
{
    public class LowercaseLetterRule : IPolicyRule
    {
        public void ValidatePolicy(object value, ref Validator validator)
        {
            if (value is string s)
            {
                if (!s.Any(char.IsLower))
                {
                    string msg = $"{validator.Name} must contain at least one lowercase letter.";
                    validator.AddException(
                        new ArgumentException(msg, validator.Name));
                }
            }
        }
    }
}
