using System;
using System.Collections.Generic;
using passwordChecker.Core.BusinessObjects.Enums;
using passwordChecker.Core.Services.Interfaces;

namespace passwordChecker.Core.Services.Implementations
{
    /// <summary>
    /// Implmentation of the password checker interface
    /// </summary>
    public class PasswordChecker : IPasswordChecker
    {
        public PasswordChecker()
        {
        }

        private const int STANDARD_PASSWORD_LENGTH = 8;
        
        public PasswordStrength GetPasswordStrength(string password)
        {
            if(null == password || 0 == password.Length)
            {
                return PasswordStrength.Blank;
            }
            else if(password.Length < STANDARD_PASSWORD_LENGTH)
            {
                return PasswordStrength.Weak;
            }
            else
            {
                var groupPresence = ComputeGroupPresence(password);
                var passwordStrength = ComputePasswordStrength(groupPresence.Keys.Count);
                return passwordStrength;
            }            
        }

        /// <summary>
        /// Checks the password for the presence of each character group.
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>Dictionary with keys for each character group</returns>
        private IDictionary<CharacterGroup, bool> ComputeGroupPresence(string password)
        {
            var groupPresence = new Dictionary<CharacterGroup, bool>();
            foreach (char c in password)
            {
                if(Char.IsLower(c))
                {
                    groupPresence[CharacterGroup.Lowercase] = true;
                }
                else if(Char.IsUpper(c))
                {
                    groupPresence[CharacterGroup.Uppercase] = true;
                }
                else if(Char.IsDigit(c))
                {
                    groupPresence[CharacterGroup.Digit] = true;
                }
                else if( isSpecialCharacter(c) )
                {
                    groupPresence[CharacterGroup.SpecialCharacter] = true;
                }
            }

            return groupPresence;
        }

        /// <summary>
        /// Computes the password strength based on number of character
        /// groups found.
        /// Strength criteria is as follows
        /// Weak for 1 group
        /// Medium for 2 groups
        /// Strong for 3 groups
        /// Very Strong for 4 groups
        /// </summary>
        /// <param name="groupCount">Number of groups</param>
        /// <returns>Password strength</returns>
        private PasswordStrength ComputePasswordStrength(int groupCount)
        {
            var passwordStrength = new PasswordStrength();
            switch(groupCount)
            {
                case 1:
                    passwordStrength = PasswordStrength.Weak;
                    break;
                case 2:
                    passwordStrength = PasswordStrength.Medium;
                    break;
                case 3:
                    passwordStrength = PasswordStrength.Strong;
                    break;
                case 4:
                    passwordStrength = PasswordStrength.VeryStrong;
                    break;
            }

            return passwordStrength;
        }

        /// <summary>
        /// Determines whether a character is a special character.
        /// Special character is either of punctuation symbols,
        /// whitespace and symbols according to UTF-8 definition.
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <returns>True if special</returns>
        /// <returns>False if not special</returns>
        private bool isSpecialCharacter(char c)
        {
            var isSpecial = Char.IsPunctuation(c);
            isSpecial = (isSpecial || Char.IsWhiteSpace(c));
            isSpecial = (isSpecial || Char.IsSymbol(c));
            return isSpecial;
        }

        
    }
}
