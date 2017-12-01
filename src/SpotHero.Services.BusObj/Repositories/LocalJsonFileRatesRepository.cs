using SpotHero.Services.BusObj.Models.Server;
using SpotHero.Services.BusObj.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotHero.Services.BusObj.Repositories
{
    public class LocalJsonFileRatesRepository : IRatesRepository
    {
        protected IJsonFileRetrievalService JsonFileRetrievalService { get; }

        protected Dictionary<DayOfWeek, List<RateForTimePeriod>> RatesDictionary { get; }

        public LocalJsonFileRatesRepository(IJsonFileRetrievalService jsonFileRetrievalService, IJsonFileParserService jsonFileParserService)
	    {
            var ratesJson = jsonFileRetrievalService.GetRatesJson();
            var rates = jsonFileParserService.GetRatesFromJson(ratesJson);
            RatesDictionary = rates.ToDictionary(r => r.Day, r => r.Rates);
            
        }

        public RateForTimePeriod GetRateForTimePeriod(DateTime startTime, DateTime endTime) 
        {
            if (startTime.Date != endTime.Date)
                return null; // not supporting overnight parking at this time. 

            if (!RatesDictionary.ContainsKey(startTime.DayOfWeek))
                return null; // garage not open

            var availableRates = RatesDictionary[startTime.DayOfWeek].OrderBy(d => d.StartTime);

            var activeRate = availableRates.FirstOrDefault(r => r.StartTime <= startTime && endTime <= r.EndTime);

            // if no match, returns null
            return activeRate;
        }
    }
}
