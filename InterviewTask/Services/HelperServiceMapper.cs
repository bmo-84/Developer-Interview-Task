using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTask.Services
{
    public class HelperServiceMapper : IHelperServiceMapper
    {
        public IEnumerable<HelperServiceViewModel> MapToViewModel(IEnumerable<HelperServiceModel> helperServices)
        {
            List<HelperServiceViewModel> result = new List<HelperServiceViewModel>();

            foreach (var helperService in helperServices)
            {
                result.Add(new HelperServiceViewModel
                {
                    Id = helperService.Id,
                    Title = helperService.Title,
                    Description = helperService.Description,
                    TelephoneNumber = helperService.TelephoneNumber,
                    Open = IsServiceOpenNow(helperService),
                    OpenUntil = (IsServiceOpenNow(helperService) == true) ? ServiceCloseTimeToday(helperService) : null,
                    NextOpen = (IsServiceOpenNow(helperService) == false) ? ServiceNextOpenTime(helperService) : null,
                    WeatherLocation = helperService.Title.Replace(" Helper Service", ""),
                });
            }

            return result;
        }

        private DateTime? ServiceNextOpenTime(HelperServiceModel helperService)
        {
            /* Maximum 7 iterations to prevent an infinite loop if the opening hours were null for all days */
            for (int i = 0; i < 7; i++)
            {
                var dayToCheck = DateTime.Now.Date.AddDays(i);
                var openingHours = GetOpeningHoursFromDate(helperService, dayToCheck);
                if (openingHours == null) return null;
                if (openingHours.Item1 > DateTime.Now & !ClosedAllDay(openingHours)) return openingHours.Item1;
            }

            return null;
        }

        private bool ClosedAllDay(Tuple<DateTime, DateTime> openingTimes)
        {
            return openingTimes.Item1 == openingTimes.Item2;
        }

        private DateTime? ServiceCloseTimeToday(HelperServiceModel helperService)
        {
            var openingHoursToday = GetOpeningHoursFromDate(helperService, DateTime.Now.Date);
            if (openingHoursToday == null) return null;
            return openingHoursToday.Item2;
        }

        private bool? IsServiceOpenNow(HelperServiceModel helperService)
        {
            var todaysHours = GetOpeningHoursFromDate(helperService, DateTime.Now);
            if (todaysHours == null) return null;
            return (todaysHours.Item1 <= DateTime.Now && DateTime.Now <= todaysHours.Item2);
        }

        private Tuple<DateTime, DateTime> GetOpeningHoursFromDate(HelperServiceModel helperService, DateTime datetime)
        {
            var openingHoursPropertyName = datetime.DayOfWeek + "OpeningHours";
            var openingHours = (List<int>)helperService.GetType().GetProperty(openingHoursPropertyName).GetValue(helperService);
            if (openingHours == null) return null;
            return new Tuple<DateTime, DateTime>(datetime.Date.AddHours(openingHours[0]), datetime.Date.AddHours(openingHours[1]));
        }
    }
}