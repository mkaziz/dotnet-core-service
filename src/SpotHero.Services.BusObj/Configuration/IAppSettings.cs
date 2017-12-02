using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Configuration
{
    public interface IAppSettings
    {
        string JsonRatesFileLocation { get; }
    }
}
