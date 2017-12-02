using System;
using System.Collections.Generic;
using System.Text;
using SpotHero.Services.BusObj.Models.Server;
using Newtonsoft.Json;
using SpotHero.Services.BusObj.Models.Client;
using System.Globalization;
using System.Linq;

namespace SpotHero.Services.BusObj.Services
{
    public class JsonFileParserService : IJsonFileParserService
    {
        public JsonFileParserService()
        {

        }

        public List<RatesForDay> GetRatesFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<RatesForDay>();

            var clientRatesList = JsonConvert.DeserializeObject<ClientRatesList>(json);
            var ratesDict = new Dictionary<DayOfWeek, List<RateForTimePeriod>>();
            
            foreach (var clientRate in clientRatesList.Rates)
            {
                var days = clientRate.Days.Split(',');

                foreach (var dayStr in days)
                {
                    var day = GetDayOfWeekFromString(dayStr);

                    if (!ratesDict.ContainsKey(day))
                        ratesDict[day] = new List<RateForTimePeriod>();

                    var ratesList = ratesDict[day];

                    var times = clientRate.Times.Split('-');
                    var startTime = DateTime.ParseExact(times[0], "HHmm", CultureInfo.InvariantCulture);
                    var endTime = DateTime.ParseExact(times[1], "HHmm", CultureInfo.InvariantCulture);

                    ratesList.Add(new RateForTimePeriod
                    {
                        Price = clientRate.Price,
                        StartTime = GetDateTimeNextWeekday(startTime, day),
                        EndTime = GetDateTimeNextWeekday(startTime, day)
                    });
                }
            }

            return ratesDict.Keys.Select(k => new RatesForDay
            {
                Day = k,
                Rates = ratesDict[k]
            }).ToList();
        }

        private static DayOfWeek GetDayOfWeekFromString(string day)
        {
            DayOfWeek result = default(DayOfWeek);
            switch(day)
            {
                case "mon":
                    result = DayOfWeek.Monday;
                    break;
                case "tues":
                    result = DayOfWeek.Tuesday;
                    break;
                case "wed":
                    result = DayOfWeek.Wednesday;
                    break;
                case "thurs":
                    result = DayOfWeek.Thursday;
                    break;
                case "fri":
                    result = DayOfWeek.Friday;
                    break;
                case "sat":
                    result = DayOfWeek.Saturday;
                    break;
                case "sun":
                    result = DayOfWeek.Sunday;
                    break;
                default:
                    throw new ArgumentException($"Day string; ${day} could not be correctly parsed into an enum");
            }

            return result;
        }

        //src: https://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
        private static DateTime GetDateTimeNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
