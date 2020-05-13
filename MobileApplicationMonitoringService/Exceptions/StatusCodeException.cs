using System;
using System.Net;

namespace MobileApplicationMonitoringService.Exceptions
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public StatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
