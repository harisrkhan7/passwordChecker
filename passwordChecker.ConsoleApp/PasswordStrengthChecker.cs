using System;
using System.Threading.Tasks;
using passwordChecker.WebAPI.Client;

namespace passwordChecker.ConsoleApp
{
    public class PasswordStrengthChecker
    {
        IPasswordCheckerAPI PasswordCheckerAPI;

        private const string FailureErrorMessage = "Could not get password strength!";

        private string Password;

        private string PromptResult;

        public PasswordStrengthChecker(IPasswordCheckerAPI passwordCheckerAPI)
        {
            PasswordCheckerAPI = passwordCheckerAPI;
        }

        private async Task<int> GetPasswordStrength()
        {
            var failure = new Exception(FailureErrorMessage);
            int? passwordStrength;
            try
            {
                passwordStrength = await PasswordCheckerAPI.Password.CheckPasswordAsync(Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw failure;
            }

            return (passwordStrength ?? throw failure);
        }

        private void PromptForPassword()
        {
            Console.WriteLine("\nPlease enter a password for strength check:");
            Password = Console.ReadLine();
        }

        private async Task DisplayResults()
        {
            try
            {
                var passwordStrength = await GetPasswordStrength();
                Console.WriteLine($"Password strength is {passwordStrength}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PromptContinue()
        {
            do
            {
                Console.WriteLine("\nDo you wish to continue? (Y/N):");
                var answer = Console.ReadKey(false).Key;
                PromptResult = answer.ToString().ToUpper();
            } while ("Y" != PromptResult && "N" != PromptResult);
        }

        public async Task SimulateUserInteraction()
        {
            Console.WriteLine("Welcome to Password Strength Calculator!");
            do
            {
                PromptForPassword();
                await DisplayResults();
                PromptContinue();
            } while ("Y" == PromptResult);
        }




    }
}
