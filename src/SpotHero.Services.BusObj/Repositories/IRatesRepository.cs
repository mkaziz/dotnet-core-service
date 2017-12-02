using SpotHero.Services.BusObj.Models.Server;
using System;
using System.Collections.Generic;

namespace SpotHero.Services.BusObj.Repositories
{
    public interface IRatesRepository
    {
        /// <summary>
        /// Get the Rate in cents for the time period provided
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        RateForTimePeriod GetRateForTimePeriod(DateTime startTime, DateTime endTime);
    }
}
