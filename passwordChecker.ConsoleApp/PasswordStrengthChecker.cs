using System;
using System.Threading.Tasks;
using passwordChecker.ConsoleApp.BusinessObjects;
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

        /// <summary>
        /// Calls the WebAPI to get password strength and breach count. 
        /// </summary>
        /// <returns>Password Response Object</returns>
        private async Task<PasswordResponse> GetPasswordResponse()
        {
            var failure = new Exception(FailureErrorMessage);
            PasswordResponse passwordResponse;
            try
            {     
                var result = await PasswordCheckerAPI.Password.CheckPasswordAsync(Password);
                var passwordResponseResult = result as passwordChecker.WebAPI.Client.Models.PasswordResponse;      
                var passwordStrengthEnum =
                    PasswordStrengthExtensions.ToPasswordStrength(passwordResponseResult.PasswordStrength);
                passwordResponse =
                    PasswordResponse.ToPasswordResponse(passwordStrengthEnum, passwordResponseResult.BreachCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(FailureErrorMessage);
                throw failure;
            }

            return passwordResponse;
        }

        /// <summary>
        /// Prompts the UI for password 
        /// </summary>
        private void PromptForPassword()
        {
            Console.WriteLine("\nPlease enter a password for strength check:");
            Password = Console.ReadLine();
        }

        /// <summary>
        /// Gets the password strength and displays it to the UI. 
        /// </summary>
        /// <returns></returns>
        private async Task DisplayResults()
        {
            try
            {
                var passwordResponse = await GetPasswordResponse();
                Console.WriteLine($"Password strength is {passwordResponse.PasswordStrength.GetDescription()}!");
                Console.WriteLine($"This password has appeared in {passwordResponse.BreachCount} breaches.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Prompts the user for another password check
        /// </summary>
        private void PromptContinue()
        {
            do
            {
                Console.WriteLine("\nDo you wish to continue? (Y/N):");
                var answer = Console.ReadKey(false).Key;
                PromptResult = answer.ToString().ToUpper();
            } while ("Y" != PromptResult && "N" != PromptResult);
        }

        /// <summary>
        /// Runs the console application interaction.
        /// Prompts for the password, displays results,
        /// prompts for continue and loops based on condition. 
        /// </summary>
        /// <returns>Completed Task When Done</returns>
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
