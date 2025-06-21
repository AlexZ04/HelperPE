using Microsoft.AspNetCore.Http;

namespace HelperPE.Common.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) :
            base(StatusCodes.Status400BadRequest, "Bad request", message)
        { }
    }
}
