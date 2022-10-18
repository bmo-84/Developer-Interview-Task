using InterviewTask.ActionFilters;
using InterviewTask.Services;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    [LoggingActionFilter]
    [ExceptionActionFilter]
    public class HomeController : Controller
    {
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */
        
        /* Ben O: I've newed up here because I couldn't see a DI framework
         * and it seemed beyond the scope of the task to add one */
        private IHelperServiceRepository _helperServiceRepository = new HelperServiceRepository();
        private IHelperServiceMapper _helperServiceMapper = new HelperServiceMapper();

        public ActionResult Index()
        {
            var helperServices = _helperServiceRepository.Get();
            var helperServiceViewModel = _helperServiceMapper.MapToViewModel(helperServices);
            return View(helperServiceViewModel);
        }
    }
}