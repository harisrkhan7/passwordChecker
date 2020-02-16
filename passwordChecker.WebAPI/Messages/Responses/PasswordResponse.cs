using System;
using System.Runtime.Serialization;

namespace passwordChecker.WebAPI.Messages.Responses
{
    [DataContract]
    public class PasswordResponse
    {
        [DataMember]
        public PasswordStrength PasswordStrength { get; set; }

        [DataMember]
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
