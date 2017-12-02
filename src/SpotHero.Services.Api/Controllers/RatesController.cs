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
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            return RatesRepository.GetRateForTimePeriod(start, end)?.Price.ToString() ?? "NOT AVAILABLE";
        }
        
    }
}
