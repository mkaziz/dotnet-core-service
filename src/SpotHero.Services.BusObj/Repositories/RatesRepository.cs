using SpotHero.Services.BusObj.Helpers;
using SpotHero.Services.BusObj.Models.Server;
using SpotHero.Services.BusObj.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotHero.Services.BusObj.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        protected IJsonFileRetrievalService JsonFileRetrievalService { get; }

        protected Dictionary<DayOfWeek, List<RateForTimePeriod>> RatesDictionary { get; }

        public RatesRepository(IJsonFileRetrievalService jsonFileRetrievalService, IJsonFileParserService jsonFileParserService)
	    {
            var ratesJson = jsonFileRetrievalService.GetRatesJson();
            var rates = jsonFileParserService.GetRatesFromJson(ratesJson);
            RatesDictionary = rates.ToDictionary(r => r.Day, r => r.Rates);
            
        }

        public RateForTimePeriod GetRateForTimePeriod(DateTime startTime, DateTime endTime)
        {
            if (startTime.Date != endTime.Date)
                return null; // not supporting overnight parking at this time. 

            if (endTime < startTime)
                throw new ArgumentOutOfRangeException($"{nameof(endTime)} must come after {nameof(startTime)}");

            if (!RatesDictionary.ContainsKey(startTime.DayOfWeek))
                return null; // garage not open

            var availableRates = RatesDictionary[startTime.DayOfWeek]
                                    .Select(r => GetNormalizedRateForTimePeriod(r, startTime, endTime))
                                    .OrderBy(d => d.StartTime);

            var activeRate = availableRates.FirstOrDefault(r => r.StartTime <= startTime && endTime <= r.EndTime);

            // commented out per Chhay's instructions
            //HandleOverlappingRate(startTime, endTime, availableRates, activeRate);

            // if no match, returns null
            return activeRate;
        }

        private static RateForTimePeriod GetNormalizedRateForTimePeriod(RateForTimePeriod rate, DateTime startTime, DateTime endTime)
        {
            var newStartTime = startTime.GetDateTimeNextWeekday(rate.StartTime.DayOfWeek);
            var newEndTime = endTime.GetDateTimeNextWeekday(rate.EndTime.DayOfWeek);

            // normalize the rate to the Day of the starttime, and the time of the original rate
            return new RateForTimePeriod
            {
                Price = rate.Price,
                StartTime = new DateTime(newStartTime.Year, newStartTime.Month, newStartTime.Day, rate.StartTime.Hour, rate.StartTime.Minute, rate.StartTime.Second),
                EndTime = new DateTime(newEndTime.Year, newEndTime.Month, newEndTime.Day, rate.EndTime.Hour, rate.EndTime.Minute, rate.EndTime.Second)
            };
        }

        // This method is not being used because overlapping rates are not allowed by the API
        private static RateForTimePeriod HandleOverlappingRate(DateTime startTime, DateTime endTime, IOrderedEnumerable<RateForTimePeriod> availableRates, RateForTimePeriod activeRate)
        {
            if (activeRate == null)
            {
                // no perfect match

                var overlappingRates = availableRates.Where(r => r.StartTime < endTime)
                                                     .Intersect(availableRates.Where(r => startTime <= r.EndTime));

                if (!overlappingRates.Any())
                    return null; // out of bounds

                var areContiguous = true;
                var seedTime = overlappingRates.FirstOrDefault().StartTime;

                foreach (var rate in overlappingRates)
                {
                    areContiguous &= rate.StartTime == seedTime;

                    if (!areContiguous)
                        return null; // garage is closed for some time

                    seedTime = rate.EndTime;
                }

                if (areContiguous)
                    return overlappingRates.OrderByDescending(r => r.Price).FirstOrDefault();
            }
            return null;
        }
    }
}
