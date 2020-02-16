using System;
namespace passwordChecker.ConsoleApp.BusinessObjects
{
    public class PasswordResponse
    {
        public PasswordStrength PasswordStrength { get; set; }

        public int BreachCount { get; set; }

        public static PasswordResponse ToPasswordResponse(PasswordStrength passwordStrength, int breachCount)
        {
            var passwordResponse = new PasswordResponse()
            {
                PasswordStrength = passwordStrength,
                BreachCount = breachCount

            };

            return passwordResponse;
        }
    }
}
