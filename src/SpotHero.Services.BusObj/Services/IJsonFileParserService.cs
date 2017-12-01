using SpotHero.Services.BusObj.Models.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Services
{
    public interface IJsonFileParserService
    {
        List<RatesForDay> GetRatesFromJson(string json);
    }
}
