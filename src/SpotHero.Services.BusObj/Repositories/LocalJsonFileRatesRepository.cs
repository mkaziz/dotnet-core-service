using SpotHero.Services.BusObj.Models.Server;
using System;
using System.Collections.Generic;

namespace SpotHero.Services.BusObj.Repositories
{
    public class LocalJsonFileRatesRepository : IRatesRepository
    {
        public LocalJsonFileRatesRepository ()
	    {

	    }

        public List<RatesForTimePeriod> GetRatesForTimePeriod(DateTime startTime, DateTime endTime) 
        {

        }
    }
}
