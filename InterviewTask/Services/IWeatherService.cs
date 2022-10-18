using InterviewTask.Models;
using System.Threading.Tasks;

namespace InterviewTask.Services
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeather(string placeName);
    }
}