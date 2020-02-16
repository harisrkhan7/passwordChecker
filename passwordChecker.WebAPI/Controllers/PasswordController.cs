using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using passwordChecker.Core.Services.Interfaces;
using passwordChecker.WebAPI.Messages.Responses;

namespace passwordChecker.WebAPI.Controllers
{
    public class PasswordController : ControllerBase
    {
        IPasswordChecker PasswordChecker;

        IBreachDataCollector BreachDataCollector;


        public PasswordController(IPasswordChecker passwordChecker, IBreachDataCollector breachDataCollector)
        {
            PasswordChecker = passwordChecker;
            BreachDataCollector = breachDataCollector;
        }

        [HttpPost("CheckPassword")]
        [ProducesResponseType(typeof(PasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckPasswordAsync(string password)
        {
            try
            {
                var passwordStrength = PasswordChecker.GetPasswordStrength(password).GetPasswordStrength();
                var breachCount = await BreachDataCollector.GetBreachCountAsync(password);
                var passwordResponse = PasswordResponse.ToPasswordResponse(passwordStrength, breachCount);
                return Ok(passwordResponse);
            }
            catch (Exception ex)
            {
                var error = ResponseManager.GetErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        [HttpPost("GetPasswordStrength")]
        [ProducesResponseType(typeof(PasswordStrength),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public IActionResult GetPasswordStrength(string password)
        {
            try
            {
                var passwordStrength = PasswordChecker.GetPasswordStrength(password);
                var passwordStrengthResponse = passwordStrength.GetPasswordStrength();
                return Ok(passwordStrengthResponse);
            }
            catch(Exception ex)
            {
                var error = ResponseManager.GetErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError,error);
            }
        }

        [HttpGet("GetBreaches")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBreaches(string password)
        {
            try
            {
                var count = await BreachDataCollector.GetBreachCountAsync(password);
                return Ok(count);
            }
            catch (Exception ex)
            {
                var error = ResponseManager.GetErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }            
        }
    }
}
