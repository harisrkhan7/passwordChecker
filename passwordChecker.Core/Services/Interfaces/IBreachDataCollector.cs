using System;
using System.Threading.Tasks;

namespace passwordChecker.Core.Services.Interfaces
{
    /// <summary>
    /// Collects the breached password hashes from the online
    /// breached password corpus and computes the number
    /// of times a password appered in the breaches.
    /// Note: This class uses K-Anonymity technique based on
    /// SHA-1 algorithm.  
    /// <see href="https://haveibeenpwned.com/API/v3#PwnedPasswords"/>
    /// </summary>
    public interface IBreachDataCollector
    {
        /// <summary>
        /// Calls the web api to get the number of times
        /// this password has appeared in breaches!
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>Number of times this password has appeared in breaches</returns>
        public Task<int> GetBreachCountAsync(string password);
    }
}
