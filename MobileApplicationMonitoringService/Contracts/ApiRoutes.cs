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
            public const string GetAll = Root + "/main";
            public const string Get = Root + "/main/{id}";
            public const string Create = Root + "/main";
            public const string Update = Root + "/main/{id}";
            public const string Delete = Root + "/main/{id}";
        }
    }
}
