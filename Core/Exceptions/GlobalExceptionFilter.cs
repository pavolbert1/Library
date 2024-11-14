using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryAPI.Core.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,

                BadRequestException => StatusCodes.Status400BadRequest,

                _ => StatusCodes.Status500InternalServerError
            };


            var message = context.Exception switch
            {
                NotFoundException => context.Exception.Message,

                BadRequestException => context.Exception.Message,

                _ => "An unexpected error occurs."
            };

            context.Result = new ObjectResult(new
            {
                message
            })
            {
                StatusCode = statusCode
            };
        }
    }

    public class NotFoundException(string message) : Exception(message)
    {
    }

    public class BadRequestException(string message) : Exception(message)
    {
    }
}