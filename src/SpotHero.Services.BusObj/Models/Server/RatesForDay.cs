using System;
using System.Collections.Generic;

namespace SpotHero.Services.BusObj.Models.Server
{
    public class RatesForDay
    {
        DayOfWeek Day { get; set; }
        List<RatesForTimePeriod> Rates { get; set; }
    }
}
