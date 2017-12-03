using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Repositories
{
    /// <summary>
    /// Used to log and retrieve statistics for API calls
    /// </summary>
    public interface IStatisticsRepository
    {
        /// <summary>
        /// Logs the time in seconds for an API call, which is represented by a string key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        void LogTime(string key, double seconds);

        /// <summary>
        /// Get an average of all times in seconds saved to a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        double GetAverageTime(string key);

        /// <summary>
        /// Gets average times in seconds for all keys stored
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<string, double>> GetAllAverageTimes();
    }
}
