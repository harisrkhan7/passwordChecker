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
    public enum PasswordStrengthResponse
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
        public static PasswordStrengthResponse GetResponseMessage(this PasswordStrength passwordStrength)
        {
            var passwordStrengthResponse = new PasswordStrengthResponse();

            switch(passwordStrength)
            {
                case PasswordStrength.Blank:
                    passwordStrengthResponse = PasswordStrengthResponse.Blank;
                    break;
                case PasswordStrength.Medium:
                    passwordStrengthResponse = PasswordStrengthResponse.Medium;
                    break;
                case PasswordStrength.Strong:
                    passwordStrengthResponse = PasswordStrengthResponse.Strong;
                    break;
                case PasswordStrength.VeryStrong:
                    passwordStrengthResponse = PasswordStrengthResponse.VeryStrong;
                    break;
            }

            return passwordStrengthResponse;
        }
    }
}