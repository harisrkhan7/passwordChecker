using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using passwordChecker.Core.Services.Implementations;
using passwordChecker.FunctionalTests.MockObjects;
using passwordChecker.WebAPI.Controllers;
using passwordChecker.WebAPI.Messages.Responses;
using Xunit;

namespace passwordChecker.FunctionalTests.Controllers
{
    public class PasswordControllerTests
    {
        private int lowerBoundaryValue = 7;

        private int upperBoundaryValue = 8;

        /// <summary>
        /// Tests with a null password. 
        /// </summary>        
        [Trait("Blank", "NoBoundary")]
        [Fact]        
        public void BlankPassword_Null()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);

            //Act
            string password = null;

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Blank, passwordStrength);
        }

        /// <summary>
        /// Tests with empty password string
        /// </summary>
        [Trait("Blank", "NoBoundary")]
        [Fact]
        public void BlankPassword_EmptyString()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);

            //Act
            string password = "";

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Blank, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from only one
        /// character group with less than the minimum required length.
        /// </summary>
        /// <param name="characterGroup">CharacterGroup to use</param>
        [Trait("SameCharacter","LowerBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase)]
        [InlineData(CharacterGroup.Uppercase)]
        [InlineData(CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Digit)]
        public void SameCharacter_LessThan8Characters(CharacterGroup characterGroup)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);

            //Act
            string password = Password.GetPassword(characterGroup, lowerBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse) okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Weak, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from only one
        /// character group with greater than or equal to
        /// the minimum required length.
        /// </summary>
        /// <param name="characterGroup">CharacterGroup to use</param>
        [Trait("SameCharacter", "UpperBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase)]
        [InlineData(CharacterGroup.Uppercase)]
        [InlineData(CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Digit)]
        public void SameCharacter_GreaterThanOrEqualTo8Characters(CharacterGroup characterGroup)
        { 
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);

            //Act
            string password = Password.GetPassword(characterGroup, upperBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Weak, passwordStrength);
        }


        /// <summary>
        /// Tests with a password having characters from two
        /// character group with less than the minimum required length.
        /// </summary>
        /// <param name="characterGroup1">Character group to use</param>
        /// <param name="characterGroup2">Character group to use</param>
        [Trait("TwoCharacterGroups", "LowerBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase,CharacterGroup.Uppercase)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Uppercase,CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Digit,CharacterGroup.SpecialCharacter)]
        public void TwoCharacterGroups_LessThan8Characters(CharacterGroup characterGroup1, CharacterGroup characterGroup2)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);

            //Act
            string password = Password.GetPassword(charGroups, lowerBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Weak, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from two
        /// character group with greater than or equal to
        /// the minimum required length.
        /// </summary>
        /// <param name="characterGroup1">Character group to use</param>
        /// <param name="characterGroup2">Character group to use</param>
        [Trait("TwoCharacterGroups", "UpperBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Uppercase)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Digit, CharacterGroup.SpecialCharacter)]
        public void TwoCharacterGroups_GreaterThanorEqualTo8Characters(CharacterGroup characterGroup1, CharacterGroup characterGroup2)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);

            //Act
            string password = Password.GetPassword(charGroups, upperBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Medium, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from three
        /// character group with less than the minimum required length.
        /// </summary>
        /// <param name="characterGroup1">Character group to use</param>
        /// <param name="characterGroup2">Character group to use</param>
        /// <param name="characterGroup3">Character group to use</param>
        [Trait("ThreeCharacterGroups", "LowerBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Uppercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Uppercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.SpecialCharacter, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.Digit, CharacterGroup.SpecialCharacter)]
        public void ThreeCharacterGroups_LessThan8Characters(CharacterGroup characterGroup1,
            CharacterGroup characterGroup2,CharacterGroup characterGroup3)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);
            charGroups.Add(characterGroup3);

            //Act
            string password = Password.GetPassword(charGroups, lowerBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Weak, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from three
        /// character group with greater than or equal to
        /// the minimum required length.
        /// </summary>
        /// <param name="characterGroup1">Character group to use</param>
        /// <param name="characterGroup2">Character group to use</param>
        /// <param name="characterGroup3">Character group to use</param>
        [Trait("ThreeCharacterGroups", "UpperBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Uppercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Uppercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.SpecialCharacter, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.Digit, CharacterGroup.SpecialCharacter)]
        public void ThreeCharacterGroups_GreaterThanorEqualTo8Characters(CharacterGroup characterGroup1,
            CharacterGroup characterGroup2, CharacterGroup characterGroup3)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);
            charGroups.Add(characterGroup3);

            //Act
            string password = Password.GetPassword(charGroups, upperBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Strong, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from four
        /// character group with less than the minimum required length.
        /// </summary>
        [Trait("FourCharacterGroups", "LowerBoundary")]
        [Fact]        
        public void FourCharacterGroups_LessThan8Characters()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(CharacterGroup.Lowercase);
            charGroups.Add(CharacterGroup.Uppercase);
            charGroups.Add(CharacterGroup.Digit);
            charGroups.Add(CharacterGroup.SpecialCharacter);

            //Act
            string password = Password.GetPassword(charGroups, lowerBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.Weak, passwordStrength);
        }

        /// <summary>
        /// Tests with a password having characters from four
        /// character group with greater than or equal to
        /// the minimum required length.
        /// </summary>
        [Trait("FourCharacterGroups", "Upper")]
        [Fact]
        public void FourCharacterGroups_GreaterThanOrEqualTo8Characters()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var controller = new PasswordController(passwordChecker);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(CharacterGroup.Lowercase);
            charGroups.Add(CharacterGroup.Uppercase);
            charGroups.Add(CharacterGroup.Digit);
            charGroups.Add(CharacterGroup.SpecialCharacter);

            //Act
            string password = Password.GetPassword(charGroups, upperBoundaryValue);

            var result = controller.CheckPassword(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordStrength = (PasswordStrengthResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrengthResponse.VeryStrong, passwordStrength);
        }
    }
}
