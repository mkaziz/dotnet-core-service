using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Repositories
{
    public interface IStatisticsRepository
    {
        void LogTime(string key, double seconds);

        double GetAverageTime(string key);

        List<KeyValuePair<string, double>> GetAllAverageTimes();
    }
}
