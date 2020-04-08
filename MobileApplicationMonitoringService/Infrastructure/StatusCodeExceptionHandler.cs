using Microsoft.AspNetCore.Http;
using MobileApplicationMonitoringService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Infrastructure
{
    public class StatusCodeExceptionHandler
    {
        private readonly RequestDelegate requestDelegate;
        public StatusCodeExceptionHandler(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (StatusCodeException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.Headers.Clear();
            }
        }
    }
}
