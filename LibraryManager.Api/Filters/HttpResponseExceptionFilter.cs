using LibraryManager.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManager.Api.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is HttpResponseException httpResponseException)
        {
            context.Result = new ObjectResult(httpResponseException.Value)
            {
                StatusCode = httpResponseException.StatusCode,
                Value = httpResponseException.Value
            };

            context.ExceptionHandled = true;
        }
    }
}
