using InterviewTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewTask.ActionFilters
{
    public class ExceptionActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            var loggingService = new LoggingService(filterContext.HttpContext.Request);
            loggingService.LogError("Error message: " + filterContext.Exception.Message + ";InnerException: " + filterContext.Exception.InnerException);
        }
    }
}