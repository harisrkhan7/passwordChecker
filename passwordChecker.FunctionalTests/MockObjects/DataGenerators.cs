using System;
namespace passwordChecker.FunctionalTests.MockObjects
{
    public static class DataGenerators
    {
        private const string LOWER_CASE = "abcdefghijklmnopqrstuvwxyz";

        private const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const string SPECIAL_CHARACTERS = @"`~!@#$%^&*()_-=+[]\;',./<>?:""{}|";

        private const string DIGIT = "0123456789";

        private static char GetRandomUpperCase()
        {
            var random = new Random();
            var randomIndex = random.Next(UPPER_CASE.Length);
            var randomUpperCase = UPPER_CASE[randomIndex];
            return randomUpperCase;
        }

        private static char GetRandomLowerCase()
        {
            var random = new Random();
            var randomIndex = random.Next(LOWER_CASE.Length);
            var randomLowerCase = LOWER_CASE[randomIndex];
            return randomLowerCase;
        }

        private static char GetRandomDigit()
        {
            var random = new Random();
            var randomIndex = random.Next(DIGIT.Length);
            var randomDigit = DIGIT[randomIndex];
            return randomDigit;
        }

        private static char GetRandomSpecialCharacter()
        {
            var random = new Random();
            var randomIndex = random.Next(SPECIAL_CHARACTERS.Length);
            var randomSpecialCharacter = SPECIAL_CHARACTERS[randomIndex];
            return randomSpecialCharacter;
        }

        public static char GetRandomCharacter(CharacterGroup characterGroup)
        {
            switch(characterGroup)
            {
                case CharacterGroup.Lowercase:
                    return GetRandomLowerCase();                    
                case CharacterGroup.Uppercase:
                    return GetRandomUpperCase();
                case CharacterGroup.SpecialCharacter:
                    return GetRandomSpecialCharacter();
                case CharacterGroup.Digit:
                    return GetRandomDigit();
                default:
                    return '\0';                    
            }
        }
    }
}
