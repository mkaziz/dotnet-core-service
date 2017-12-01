using SpotHero.Services.BusObj.Models.Server;
using System;
using System.Collections.Generic;

namespace SpotHero.Services.BusObj.Repositories
{
    public interface IRatesRepository
    {
        List<RatesForTimePeriod> GetRatesForTimePeriod(DateTime startTime, DateTime endTime);
    }
}
