using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Exceptions
{
    public class StatusCodeException:Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public StatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
