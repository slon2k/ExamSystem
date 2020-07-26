using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Errors
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    string result = "";
                    switch (exception)
                    {
                        case RestException re:
                            context.Response.StatusCode = (int)re.Code;
                            result = JsonConvert.SerializeObject(new { re.Errors });
                            break;
                        case Exception e:
                            context.Response.StatusCode = 500;
                            result = JsonConvert.SerializeObject(new { error = exception.Message });
                            break;
                    }
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                });
            });
        }
    }
}
