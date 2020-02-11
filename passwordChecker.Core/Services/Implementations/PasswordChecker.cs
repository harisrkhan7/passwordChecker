using System;
using passwordChecker.Core.BusinessObjects.Enums;
using passwordChecker.Core.Services.Interfaces;

namespace passwordChecker.Core.Services.Implementations
{
    /// <summary>
    /// Implmentation of the password checker interface
    /// </summary>
    public class PasswordChecker : IPasswordChecker
    {
        public PasswordChecker()
        {
        }

        public PasswordStrength GetPasswordStrength(string password)
        {
            throw new NotImplementedException();
        }
    }
}
