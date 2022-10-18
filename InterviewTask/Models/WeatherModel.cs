using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class WeatherModel
    {
        public string Summary { get; internal set; }
        public string IconPath { get; internal set; }
        public int Temperature { get; internal set; }
    }
}