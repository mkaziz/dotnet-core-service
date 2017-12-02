using SpotHero.Services.BusObj.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Services
{
    public interface IJsonFileParserService
    {
        /// <summary>
        /// Parses provided JSON into Rates model objects
        /// </summary>
        /// <param name="json">A list of rates for the days in the JSON</param>
        /// <returns></returns>
        List<RatesForDay> GetRatesFromJson(string json);
    }
}
