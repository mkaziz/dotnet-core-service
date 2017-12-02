using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotHero.Services.BusObj.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        protected ConcurrentDictionary<string, List<double>> AverageTimesDict { get; set; }

        public StatisticsRepository()
        {
            AverageTimesDict = new ConcurrentDictionary<string, List<double>>();
        }

        public void LogTime(string key, double seconds)
        {
            if (!AverageTimesDict.ContainsKey(key))
                AverageTimesDict[key] = new List<double> { seconds };
            else
                AverageTimesDict[key].Add(seconds);
        }

        public double GetAverageTime(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException($"Key: {key} not present");

            return AverageTimesDict[key].Average();
        }

        public List<KeyValuePair<string, double>> GetAllAverageTimes()
        {
            return AverageTimesDict.Keys.Select(k => new KeyValuePair<string, double>(k, GetAverageTime(k))).ToList();
        }


    }
}
