using SpotHero.Services.BusObj.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Services
{
    public class JsonFileRetrievalService : IJsonFileRetrievalService
    {
        protected string JsonRates { get; }

        public JsonFileRetrievalService(IAppSettings appSettings)
        {
            JsonRates = System.IO.File.ReadAllText(appSettings.JsonRatesFileLocation);
        }

        public string GetRatesJson()
        {
            return JsonRates;
        }
    }
}
