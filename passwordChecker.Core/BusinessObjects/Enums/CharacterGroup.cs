using System;
using System.ComponentModel;

namespace passwordChecker.Core.BusinessObjects.Enums
{
    /// <summary>
    /// All the character groups for password checks
    /// </summary>
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

    
}
