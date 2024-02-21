using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookApp
{
    public class GlobalExceptionHandler
    {
        public async Task InvokeAsync(HttpContext context, Func<Task> next)
        {
            try
            {
               await next();
            }
            catch (Exception ex)
            {
              await  HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync("{\"message\":\"Unknown Error Occurred\"}");
        }
    }
}
