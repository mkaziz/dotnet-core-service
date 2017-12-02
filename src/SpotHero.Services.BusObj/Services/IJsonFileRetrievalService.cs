using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Services
{
    public interface IJsonFileRetrievalService
    {
        /// <summary>
        /// Retrieves json data for rates
        /// </summary>
        /// <returns></returns>
        string GetRatesJson();
    }
}
