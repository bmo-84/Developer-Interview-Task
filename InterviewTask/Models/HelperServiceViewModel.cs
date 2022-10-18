using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class HelperServiceViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }

        public bool? Open { get; set; }
        public DateTime? OpenUntil { get; set; }
        public DateTime? NextOpen { get; set; }
        public string WeatherLocation { get; set; }
    }
}