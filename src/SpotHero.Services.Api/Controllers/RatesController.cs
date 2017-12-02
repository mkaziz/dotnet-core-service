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
        public RatesController(IRatesRepository ratesRepository)
        {
            RatesRepository = ratesRepository;
        }
        // GET api/values
        [HttpGet]
        public string Get(string startDate, string endDate)
        {
            DateTime start;
            DateTime end;

            if (!DateTime.TryParse(startDate, out start))
                throw new ArgumentException($"Could not parse ${nameof(startDate)}: {startDate} into a DateTime");

            if (!DateTime.TryParse(endDate, out end))
                throw new ArgumentException($"Could not parse ${nameof(endDate)}: {endDate} into a DateTime");

            return RatesRepository.GetRateForTimePeriod(start, end)?.Price.ToString() ?? "NOT AVAILABLE";
        }
        
    }
}
