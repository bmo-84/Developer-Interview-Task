using InterviewTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService = new WeatherService(); // "new" should be replaced with dependency injection e.g. Autofac
        
        public async Task<ActionResult> Index(string city)
        {
            var weather = await _weatherService.GetWeather(city);
            return Json(weather, JsonRequestBehavior.AllowGet);
        }
    }
}