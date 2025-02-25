using Microsoft.AspNetCore.Diagnostics;
using Milliygramm.Model.ApiModels;

public class InternalServerExceptionHandler(ILogger<InternalServerExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = 500,
            Message = exception.Message,
        });

        //if (exception is not AlreadyExistException 
        //    && exception is not CustomException 
        //    && exception is not NotFoundException 
        //    && exception is not ArgumentIsNotValidException)
        //{
        //  //  logger.LogError(exception.Message);
        //}

        return true;
    }
}