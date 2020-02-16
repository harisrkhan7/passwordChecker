using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using passwordChecker.Core.BusinessObjects.Enums;

namespace passwordChecker.WebAPI.Messages.Responses
{
    /// <summary>
    /// Password strength enum
    /// </summary>
    [DataContract]
    public enum PasswordStrength
    {
        [EnumMember(Value = "Blank")]
        Blank = 0,
        [EnumMember(Value = "Weak")]
        Weak,
        [EnumMember(Value = "Medium")]
        Medium,
        [EnumMember(Value = "Strong")]
        Strong,
        [EnumMember(Value = "Very Strong")]
        VeryStrong
    }

    public static class PasswordStrengthResponseExtensions
    {
        /// <summary>
        /// Converts Business object to response message
        /// </summary>
        /// <param name="passwordStrength">Business object to convert</param>
        /// <returns>Response message</returns>
        public static PasswordStrength GetPasswordStrength( this Core.BusinessObjects.Enums.PasswordStrength passwordStrength)
        {
            var passwordStrengthResponse = new PasswordStrength();

            switch(passwordStrength)
            {
                case Core.BusinessObjects.Enums.PasswordStrength.Blank:
                    passwordStrengthResponse = PasswordStrength.Blank;
                    break;
                case Core.BusinessObjects.Enums.PasswordStrength.Weak:
                    passwordStrengthResponse = PasswordStrength.Weak;
                    break;
                case Core.BusinessObjects.Enums.PasswordStrength.Medium:
                    passwordStrengthResponse = PasswordStrength.Medium;
                    break;
                case Core.BusinessObjects.Enums.PasswordStrength.Strong:
                    passwordStrengthResponse = PasswordStrength.Strong;
                    break;
                case Core.BusinessObjects.Enums.PasswordStrength.VeryStrong:
                    passwordStrengthResponse = PasswordStrength.VeryStrong;
                    break;
                default:
                    throw new Exception("Could not parse to response message! Case = " + passwordStrength);
            }

            return passwordStrengthResponse;
        }
    }
}