using System;

namespace SpotHero.Services.BusObj.Models.Server
{
    public class RateForTimePeriod
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Decimal Price { get; set; }
    }
}
