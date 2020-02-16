using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using passwordChecker.Core.Services.Interfaces;

namespace passwordChecker.Core.Services.Implementations
{
    public class BreachDataCollector : IBreachDataCollector
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly string COLON = ":";

        private const string CouldNotGetBreachesErrorMessage =
            "Could Not Get Breaches From The Server!";
        public BreachDataCollector(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<int> GetBreachCountAsync(string password)
        {
            try
            {
                if(null == password || "" == password)
                {
                    return 0;
                }

                var hash = ComputeHash(password);

                //Use first 5 characters to get matching breaches
                var hashPrefix = hash.Substring(startIndex: 0, length: 5);
                var hashSuffix = hash.Substring(startIndex: 5, length: 35);

                var breaches = await GetMatchingBreachesAsync(hashPrefix);
                var breachDictionary = ParseMatchingBreachesAsync(breaches);

                var totalBreaches = ComputeTotalBreaches(breachDictionary, hashSuffix);
                return totalBreaches;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string ComputeHash(string password)
        {
            //Get bytes of password string
            var passwordByteArray = System.Text.Encoding.UTF8.GetBytes(password);

            //Compute SHA 1 Hash
            var sha = new SHA1CryptoServiceProvider();
            var result = sha.ComputeHash(passwordByteArray);
            var hash = BitConverter.ToString(result).Replace("-","");
            return hash;
        }

        private async Task<string[]> GetMatchingBreachesAsync(string hash)
        {
            string[] breachesList;
            try
            {
                var getUrl = $"/range/{hash}";

                var client = _clientFactory.CreateClient(Constants.DATA_BREACH_CLIENT_NAME);

                var result = await client.GetAsync(getUrl);
                result.EnsureSuccessStatusCode();

                var resultContent = await result.Content.ReadAsStringAsync();
                breachesList = resultContent.Split(Environment.NewLine);

            }
            catch (Exception ex)
            {
                throw new Exception(CouldNotGetBreachesErrorMessage);
            }

            return breachesList;
        }

        private IDictionary<string, int> ParseMatchingBreachesAsync(string[] breachList)
        {
            var breachDictionary = new Dictionary<string, int>();
            foreach(var breach in breachList)
            {
                try
                {
                    var breachSplit = breach.Split(COLON);

                    if (2 != breachSplit.Length)
                    {
                        //Invalid data from API
                        continue;
                    }

                    var hashSuffix = breachSplit[0];
                    var count = int.Parse(breachSplit[1]);

                    //Handle duplicates
                    if(breachDictionary.ContainsKey(hashSuffix))
                    {
                        breachDictionary[hashSuffix] += count;
                    }
                    else
                    {
                        breachDictionary[hashSuffix] = count;
                    }                    

                }catch(Exception ex)
                {
                    //Ignore any errors and try to parse the entire dataset
                    continue;
                }                               
            }

            return breachDictionary;
        }

        private int ComputeTotalBreaches(IDictionary<string, int> collectedBreaches, string passwordHashSuffix)
        {
            int totalBreaches;
            if(collectedBreaches.ContainsKey(passwordHashSuffix))
            {
                totalBreaches = collectedBreaches[passwordHashSuffix];
            }
            else
            {
                totalBreaches = 0;
            }

            return totalBreaches;
        }

      
    }
}
