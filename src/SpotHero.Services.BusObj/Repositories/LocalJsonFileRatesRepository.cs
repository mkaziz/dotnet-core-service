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

        protected Dictionary<DayOfWeek, List<RateForTimePeriod>> RatesDictionary { get; } = new Dictionary<DayOfWeek, List<RateForTimePeriod>>();

        public LocalJsonFileRatesRepository(IJsonFileRetrievalService jsonFileRetrievalService)
	    {
            JsonFileRetrievalService = jsonFileRetrievalService;
	    }

        public RateForTimePeriod GetRateForTimePeriod(DateTime startTime, DateTime endTime) 
        {
            if (startTime.Date != endTime.Date)
                return null; // not supporting overnight parking at this time. 

            if (!RatesDictionary.ContainsKey(startTime.DayOfWeek))
                return null; // garage not open

            var availableRates = RatesDictionary[startTime.DayOfWeek].OrderBy(d => d.StartTime);

            var activeRate = availableRates.FirstOrDefault(r => r.StartTime >= startTime && r.EndTime <= endTime);

            // if no match, returns null
            return activeRate;
        }
    }
}
