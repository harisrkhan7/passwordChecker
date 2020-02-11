using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using passwordChecker.Core.Services.Interfaces;

namespace passwordChecker.WebAPI.Controllers
{
    public class PasswordController : ControllerBase
    {
        IPasswordChecker PasswordChecker;

        public PasswordController(IPasswordChecker passwordChecker)
        {
            PasswordChecker = passwordChecker;
        }
        
        [HttpPost("CheckPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CheckPassword(string password)
        {
            try
            {
                var passwordStrength = PasswordChecker.GetPasswordStrength(password);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
