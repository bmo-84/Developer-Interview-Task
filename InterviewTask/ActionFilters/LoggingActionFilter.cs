using InterviewTask.Services;
using System.Web.Mvc;

namespace InterviewTask.ActionFilters
{
    public class LoggingActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            var loggingService = new LoggingService(filterContext.HttpContext.Request);
            loggingService.LogInformation("Returning response: " + filterContext.HttpContext.Response.StatusCode);
        }
    }
}