using InterviewTask.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InterviewTask.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherModel> GetWeather(string placeName)
        {
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["WeatherApiKey"];

            using (var client = new HttpClient())
            {
                HttpResponseMessage response;
                
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    response = await client.GetAsync($"/data/2.5/weather?q={placeName}&appid={apiKey}&units=metric");
                    response.EnsureSuccessStatusCode();     
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new WeatherServiceException("Unable to retrieve weather from OpenWeather", httpRequestException);
                }

                try
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(stringResult);

                    var weather = jsonObject["weather"][0];
                    var temperature = jsonObject["main"]["temp"];

                    var result = new WeatherModel
                    {
                        Summary = weather["main"].ToString(),
                        IconPath = String.Format("http://openweathermap.org/img/wn/{0}.png", weather["icon"]),
                        Temperature = (int)Math.Round((double)temperature)
                    };

                    return result;
                }
                
                catch (JsonException jsonException)
                {
                    throw new WeatherServiceException("Unable to parse weather from OpenWeather response", jsonException);
                }
            }

        }
    }

    public class WeatherServiceException : Exception
    {
        public WeatherServiceException()
        {
        }

        public WeatherServiceException(string message)
            : base(message)
        {
        }

        public WeatherServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}