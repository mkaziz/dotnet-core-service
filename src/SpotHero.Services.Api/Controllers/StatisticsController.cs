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

        [HttpGet]
        [Route("GetByKey")]
        public string GetByKey(string key)
        {
            try
            {
                return $"Average Response Time: {StatisticsRepository.GetAverageTime(key)}";
            }
            catch (ApplicationException)
            {
                return "No key passed in";
            }
            catch (KeyNotFoundException)
            {
                return $"{key} is valid a valid key";
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<string> GetAll()
        {
            return StatisticsRepository.GetAllAverageTimes().Select(k => $"{k.Key} - {k.Value}");
        }

    }
}
