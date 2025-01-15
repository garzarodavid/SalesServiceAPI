using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ILogger = Serilog.ILogger;

namespace SalesServiceAPI.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger _logger;

    public ExceptionFilter(ILogger logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var errorResponse = new
        {
            message = "An error occurred while processing your request.",
            details = exception.Message
        };

        _logger.Error(exception, "An error occurred in the action execution.");

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}
