using System;
using passwordChecker.WebAPI.Messages.Responses;

namespace passwordChecker.WebAPI
{
    public class ResponseManager
    {
        public ResponseManager()
        {
        }

        public static ErrorResponse GetErrorResponse(Exception exception)
        {
            var error = new ErrorResponse()
            {
                Message = exception.Message
            };

            if(null != exception.InnerException)
            {
                error.Message += " " + exception.InnerException;
            }
            return error;
        }


    }
}
