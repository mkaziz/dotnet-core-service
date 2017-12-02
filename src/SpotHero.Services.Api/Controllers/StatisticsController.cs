using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotHero.Services.BusObj.Repositories;
using Newtonsoft.Json;

namespace SpotHero.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        protected IStatisticsRepository StatisticsRepository { get; }
        public StatisticsController(IRatesRepository ratesRepository, IStatisticsRepository statisticsRepository)
        {
            StatisticsRepository = statisticsRepository;
        }
        // GET api/values
        [HttpGet]
        [Route("GetByKey")]
        public string GetByKey(string key)
        {
            return $"Average Response Time: {StatisticsRepository.GetAverageTime(key)}";
        }

        // GET api/values
        [HttpGet]
        [Route("GetAll")]
        public List<KeyValuePair<string, double>> GetAll()
        {
            return StatisticsRepository.GetAllAverageTimes();
        }

    }
}
