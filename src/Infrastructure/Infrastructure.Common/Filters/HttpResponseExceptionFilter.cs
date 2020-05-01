using Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                var controller = context.Controller as ControllerBase;
                context.Result = controller.Problem(
                    detail: exception.Message,
                    statusCode: exception.Status,
                    title: "One or more errors occured.");
                context.ExceptionHandled = true;
            }
        }
    }
}
