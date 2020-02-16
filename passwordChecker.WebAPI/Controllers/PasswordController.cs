using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using passwordChecker.Core.Services.Interfaces;
using passwordChecker.WebAPI.Messages.Responses;

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
        [ProducesResponseType(typeof(PasswordStrengthResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public IActionResult CheckPassword(string password)
        {
            try
            {
                var passwordStrength = PasswordChecker.GetPasswordStrength(password);
                var passwordStrengthResponse = passwordStrength.GetResponseMessage();
                return Ok(passwordStrengthResponse);
            }
            catch(Exception ex)
            {
                var error = ResponseManager.GetErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError,error);
            }
        }
    }
}
