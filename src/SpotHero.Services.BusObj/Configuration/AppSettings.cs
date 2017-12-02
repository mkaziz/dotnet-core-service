using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Configuration
{
    public class AppSettings : IAppSettings
    {
        public string JsonRatesFileLocation { get; set; }
    }
}
