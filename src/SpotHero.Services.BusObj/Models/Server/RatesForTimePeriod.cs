using System;

namespace SpotHero.Services.BusObj.Models.Server
{
    public class RatesForTimePeriod
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }

        Decimal Price { get; }
    }
}
