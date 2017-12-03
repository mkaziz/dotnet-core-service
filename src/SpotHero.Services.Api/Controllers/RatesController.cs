using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotHero.Services.BusObj.Repositories;

namespace SpotHero.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
        protected IRatesRepository RatesRepository { get; }
        protected IStatisticsRepository StatisticsRepository { get; }
        public RatesController(IRatesRepository ratesRepository, IStatisticsRepository statisticsRepository)
        {
            StatisticsRepository = statisticsRepository;
            RatesRepository = ratesRepository;
        }

        [HttpGet]
        public string Get(string startDate, string endDate)
        {
            var logStartTime = DateTime.Now;

            DateTime start;
            DateTime end;

            if (!DateTime.TryParse(startDate, out start))
                throw new ArgumentException($"Could not parse ${nameof(startDate)}: {startDate} into a DateTime");

            if (!DateTime.TryParse(endDate, out end))
                throw new ArgumentException($"Could not parse ${nameof(endDate)}: {endDate} into a DateTime");

            var logEndTime = DateTime.Now;

            var rate = RatesRepository.GetRateForTimePeriod(start, end)?.Price.ToString() ?? "NOT AVAILABLE";

            StatisticsRepository.LogTime($"api/rates", (logEndTime - logStartTime).TotalSeconds);

            return rate;
        }
        
    }
}
