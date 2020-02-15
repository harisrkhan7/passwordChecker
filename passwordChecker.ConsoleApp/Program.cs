using System;
using System.Threading.Tasks;
using passwordChecker.WebAPI.Client;

namespace passwordChecker.ConsoleApp
{
    class Program
    {
        private const string clientURL = "https://localhost:5001";
        static async Task Main(string[] args)
        {
            var baseUri = new Uri(clientURL);

            var passwordCheckerAPI = new PasswordCheckerAPI(baseUri);
            var passwordSimulator = new PasswordStrengthChecker(passwordCheckerAPI);

            await passwordSimulator.SimulateUserInteraction();           
        }
    }
}
