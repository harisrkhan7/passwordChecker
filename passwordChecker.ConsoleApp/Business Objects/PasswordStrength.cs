using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace passwordChecker.ConsoleApp.BusinessObjects
{
    public enum PasswordStrength
    {
        [Description("Blank")]
        Blank = 0,
        [Description("Weak")]
        Weak,
        [Description("Medium")]
        Medium,
        [Description("Strong")]
        Strong,
        [Description("Very Strong")]
        VeryStrong
    }

    public static class PasswordStrengthExtensions
    {
        private const string PasswordStrengthErrorMessage
            = "Could Not Parse Password Strength! Password strength is null";

        private const string PasswordStrengthUnknownErrorMessage
            = "Could Not Parse Password Strength Returned From the Backend System! Password strength is unknown.";
        public static PasswordStrength ToPasswordStrength(int? strength)
        {
            int passwordStrength = strength ?? throw new Exception(PasswordStrengthErrorMessage);
            var passwordStrengthEnum = new PasswordStrength();
            switch (passwordStrength)
            {
                case 0:
                    passwordStrengthEnum = PasswordStrength.Blank;
                    break;
                case 1:
                    passwordStrengthEnum = PasswordStrength.Weak;
                    break;
                case 2:
                    passwordStrengthEnum = PasswordStrength.Medium;
                    break;
                case 3:
                    passwordStrengthEnum = PasswordStrength.Strong;
                    break;
                case 4:
                    passwordStrengthEnum = PasswordStrength.VeryStrong;
                    break;
                default:
                    throw new Exception(PasswordStrengthErrorMessage);
            }

            return passwordStrengthEnum;
        }


        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }

    }
}
