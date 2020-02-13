using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace passwordChecker.FunctionalTests.MockObjects
{
    public enum CharacterGroup
    {
        [Description("Lowercase")]
        Lowercase = 1,
        [Description("Uppercase")]
        Uppercase,
        [Description("Digit")]
        Digit,
        [Description("Special Character")]
        SpecialCharacter
    }

    public static class CharacterGroupExtensions
    {
        public static List<CharacterGroup> GetRandomCharacterGroups(int numberOfCharGroups)
        {
            var values = Enum.GetValues(typeof(CharacterGroup));
            Random random = new Random();
            var characterGroups = new List<CharacterGroup>();

            for (int i = 0; i < numberOfCharGroups; i++)
            {
                CharacterGroup randomCharacterGroup = (CharacterGroup)values.GetValue(random.Next(values.Length));
                characterGroups.Add(randomCharacterGroup);
            }
            return characterGroups;
        }
    }

}
