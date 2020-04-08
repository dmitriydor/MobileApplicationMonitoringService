using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class IdentificationData
        {
            private const string Path = "/identification";
            public const string GetAll = Root + Path;
            public const string Get = Root + Path +"/{id:Guid}";
            public const string Create = Root + Path;
            public const string Update = Root + Path + "/{id:Guid}";
            public const string Delete = Root + Path + "/{id:Guid}";
        }
        public static class Event
        {
            private const string Path = "/event";
            public const string GetAll = Root + Path;
            public const string GetAllFor = Root + Path + "/{identificationId:Guid}";
            public const string Create = Root + Path;
            public const string Update = Root + Path + "/{id:Guid}";
        }
    }
}
