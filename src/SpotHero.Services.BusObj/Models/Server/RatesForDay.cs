using System;
using System.Collections.Generic;

namespace SpotHero.Services.BusObj.Models.Server
{
    public class RatesForDay
    {
        public DayOfWeek Day { get; set; }
        public List<RateForTimePeriod> Rates { get; set; }
    }
}
