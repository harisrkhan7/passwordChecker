using System;
using System.Collections.Generic;

namespace passwordChecker.FunctionalTests.MockObjects
{
    public class Password
    {
        public Password()
        {
        }

        /// <summary>
        /// Generate random password of one character group
        /// </summary>
        /// <param name="characterGroup">Character group to use</param>
        /// <param name="length">Length of password</param>
        /// <returns>Random password of given character group</returns>
        public static string GetPassword(CharacterGroup characterGroup, int length)
        {
            string password = null;

            for(int i=0;i<length;i++)
            {
                password += DataGenerators.GetRandomCharacter(characterGroup);
            }
            return password;
        }
        
        /// <summary>
        /// Get a character from each group in a round robin fashion
        /// till end of array is reached.
        /// </summary>
        /// <param name="characterGroups"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetPassword(List<CharacterGroup> characterGroups, int length)
        {
            string password = null;
            int characterGroupsIterator = 0;
            
            for(int i=0;i<length;i++)
            {
                var currentCharacterGroup = characterGroups[characterGroupsIterator];
                password += DataGenerators.GetRandomCharacter(currentCharacterGroup);
                characterGroupsIterator = ((characterGroupsIterator + 1) % characterGroups.Count);
            }

            return password;
        }

    }
}
