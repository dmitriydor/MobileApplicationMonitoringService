using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MobileApplicationMonitoringService.Filters
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Data object is null");
            }
            if (context.HttpContext.Request.Body == null)
            {
                context.Result = new BadRequestObjectResult("Invalid model object");
            }
        }
    }
}
