using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

        private string dataBreachUrl = @"https://api.pwnedpasswords.com/";

        /// <summary>
        /// Tests with a null password. 
        /// </summary>        
        [Trait("Blank", "NoBoundary")]
        [Fact]
        public async Task BlankPassword_Null()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);

            //Act
            string password = null;

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Blank, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
        }

        /// <summary>
        /// Tests with empty password string
        /// </summary>
        [Trait("Blank", "NoBoundary")]
        [Fact]
        public async Task BlankPassword_EmptyString()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);

            //Act
            string password = "";

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Blank, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);

        }

        /// <summary>
        /// Tests with a password having characters from only one
        /// character group with less than the minimum required length.
        /// </summary>
        /// <param name="characterGroup">CharacterGroup to use</param>
        [Trait("SameCharacter", "LowerBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase)]
        [InlineData(CharacterGroup.Uppercase)]
        [InlineData(CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Digit)]
        public async Task SameCharacter_LessThan8Characters(CharacterGroup characterGroup)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);

            //Act
            string password = Password.GetPassword(characterGroup, lowerBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Weak, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
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
        public async Task SameCharacter_GreaterThanOrEqualTo8Characters(CharacterGroup characterGroup)
        {
            ///Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);

            //Act
            string password = Password.GetPassword(characterGroup, upperBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Weak, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
        }


        /// <summary>
        /// Tests with a password having characters from two
        /// character group with less than the minimum required length.
        /// </summary>
        /// <param name="characterGroup1">Character group to use</param>
        /// <param name="characterGroup2">Character group to use</param>
        [Trait("TwoCharacterGroups", "LowerBoundary")]
        [Theory]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Uppercase)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Lowercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.Digit)]
        [InlineData(CharacterGroup.Uppercase, CharacterGroup.SpecialCharacter)]
        [InlineData(CharacterGroup.Digit, CharacterGroup.SpecialCharacter)]
        public async Task TwoCharacterGroups_LessThan8Characters(CharacterGroup characterGroup1, CharacterGroup characterGroup2)
        {
            //Arrange 
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);

            //Act
            string password = Password.GetPassword(charGroups, lowerBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Weak, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
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
        public async Task TwoCharacterGroups_GreaterThanorEqualTo8Characters(CharacterGroup characterGroup1, CharacterGroup characterGroup2)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);

            //Act
            string password = Password.GetPassword(charGroups, upperBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Medium, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
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
        public async Task ThreeCharacterGroups_LessThan8Characters(CharacterGroup characterGroup1,
            CharacterGroup characterGroup2, CharacterGroup characterGroup3)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);
            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);
            charGroups.Add(characterGroup3);

            //Act
            string password = Password.GetPassword(charGroups, lowerBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Weak, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
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
        public async Task ThreeCharacterGroups_GreaterThanorEqualTo8Characters(CharacterGroup characterGroup1,
            CharacterGroup characterGroup2, CharacterGroup characterGroup3)
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);

            var charGroups = new List<CharacterGroup>();
            charGroups.Add(characterGroup1);
            charGroups.Add(characterGroup2);
            charGroups.Add(characterGroup3);

            //Act
            string password = Password.GetPassword(charGroups, upperBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Strong, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
        }

        /// <summary>
        /// Tests with a password having characters from four
        /// character group with less than the minimum required length.
        /// </summary>
        [Trait("FourCharacterGroups", "LowerBoundary")]
        [Fact]
        public async Task FourCharacterGroups_LessThan8Characters()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);

            var charGroups = new List<CharacterGroup>();
            charGroups.Add(CharacterGroup.Lowercase);
            charGroups.Add(CharacterGroup.Uppercase);
            charGroups.Add(CharacterGroup.Digit);
            charGroups.Add(CharacterGroup.SpecialCharacter);

            //Act
            string password = Password.GetPassword(charGroups, lowerBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.Weak, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
        }

        /// <summary>
        /// Tests with a password having characters from four
        /// character group with greater than or equal to
        /// the minimum required length.
        /// </summary>
        [Trait("FourCharacterGroups", "Upper")]
        [Fact]
        public async Task FourCharacterGroups_GreaterThanOrEqualTo8Characters()
        {
            //Arrange 
            var passwordChecker = new PasswordChecker();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(dataBreachUrl)
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);


            var breachDataCollector = new BreachDataCollector(httpClientFactory.Object);

            var controller = new PasswordController(passwordChecker, breachDataCollector);


            var charGroups = new List<CharacterGroup>();
            charGroups.Add(CharacterGroup.Lowercase);
            charGroups.Add(CharacterGroup.Uppercase);
            charGroups.Add(CharacterGroup.Digit);
            charGroups.Add(CharacterGroup.SpecialCharacter);

            //Act
            string password = Password.GetPassword(charGroups, upperBoundaryValue);

            var result = await controller.CheckPasswordAsync(password);

            //Assert

            var okObjectResult = result as OkObjectResult;

            var passwordResponse = (PasswordResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.Equal(PasswordStrength.VeryStrong, passwordResponse.PasswordStrength);
            Assert.True(passwordResponse.BreachCount >= 0);
        }
    }
}
