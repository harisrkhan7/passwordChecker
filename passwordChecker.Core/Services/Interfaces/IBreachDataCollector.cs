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
        public Task<int> GetBreachCount(string password);
    }
}
