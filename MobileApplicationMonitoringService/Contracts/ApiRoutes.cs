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
            public const string GetAll = Root + "/identification";
            public const string Get = Root + "/identification/{id:Guid}";
            public const string Create = Root + "/identification";
            public const string Update = Root + "/identification/{id:Guid}";
            public const string Delete = Root + "/identification/{id:Guid}";
        }
    }
}
