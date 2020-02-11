using System;
using passwordChecker.Core.BusinessObjects.Enums;

namespace passwordChecker.Core.Services.Interfaces
{
    /// <summary>
    /// Helper functions for checking password. 
    /// </summary>
    public interface IPasswordChecker
    {
        /// <summary>
        /// Computes password strength. 
        /// </summary>
        /// <param name="password">Password string to evaluate</param>
        /// <returns>Password strength enum</returns>
        PasswordStrength GetPasswordStrength(string password);
    }
}
