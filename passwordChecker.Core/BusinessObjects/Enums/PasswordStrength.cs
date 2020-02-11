using System;
using System.ComponentModel;

namespace passwordChecker.Core.BusinessObjects.Enums
{
    /// <summary>
    /// Defines the strength of a password.
    /// </summary>
    public enum PasswordStrength
    {
        [Description("Blank")]
        Blank = 0,
        [Description("Weak")]
        Weak,
        [Description("Medium")]
        Medium,
        [Description("Strong")]
        Strong,
        [Description("VeryStrong")]
        VeryStrong
    }
}
