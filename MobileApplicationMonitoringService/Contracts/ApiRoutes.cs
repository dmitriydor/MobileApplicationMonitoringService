using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class ApplicationData
        {
            private const string Path = "/applications";
            public const string GetAll = Root + Path;
            public const string Get = Root + Path +"/{id:Guid}";
            public const string Delete = Root + Path + "/{id:Guid}";
        }
        public static class ApplicationEvent
        {
            private const string Path = "/events";
            public const string GetAll = Root + Path;
            public const string GetAllFor = Root + Path + "/{applicationId:Guid}";
            public const string Create = Root + Path;
            public const string Update = Root + Path + "/{id:Guid}";
        }
        public static class Statistics
        {
            private const string Path = "/statistics";
            public const string Create = Root + Path;
            public const string Update = Root + Path + "/{id:Guid}";
        }
    }
}
