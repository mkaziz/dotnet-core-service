using NSubstitute;
using SpotHero.Services.BusObj.Models.Server;
using SpotHero.Services.BusObj.Repositories;
using SpotHero.Services.BusObj.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.Tests.Repositories.LocalJsonFileRatesRepositoryTests
{
    public class LocalJsonFileRatesRepositoryMockBuilder
    {
        protected IJsonFileRetrievalService JsonFileRetrievalService { get; set; }
        protected IJsonFileParserService JsonFileParserService { get; set; }

        public LocalJsonFileRatesRepositoryMockBuilder()
        {
            JsonFileRetrievalService = Substitute.For<IJsonFileRetrievalService>();
            JsonFileParserService = Substitute.For<IJsonFileParserService>();

            FillDefaultData();
        }

        private void FillDefaultData()
        {
            JsonFileParserService.GetRatesFromJson(Arg.Any<string>()).Returns(new List<RatesForDay>
            {
                new RatesForDay
                {
                    Day = DayOfWeek.Monday,
                    Rates = new List<RateForTimePeriod>
                    {
                       new RateForTimePeriod
                       {
                           StartTime = DateTime.Parse("Nov 27 2017, 4AM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 12PM"),
                           Price = 1500
                       },
                       new RateForTimePeriod
                       {
                           StartTime = DateTime.Parse("Nov 27 2017, 4PM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 6PM"),
                           Price = 2500
                       },
                    }
                },
                new RatesForDay
                {
                    Day = DayOfWeek.Tuesday,
                    Rates = new List<RateForTimePeriod>
                    {
                       new RateForTimePeriod
                       {
                           StartTime = DateTime.Parse("Nov 27 2017, 4AM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 12PM"),
                           Price = 1500
                       },
                       new RateForTimePeriod
                       {
                           StartTime = DateTime.Parse("Nov 27 2017, 12PM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 6PM"),
                           Price = 2500
                       },
                    }
                }
            });
        }

        public LocalJsonFileRatesRepositoryMockBuilder BuildWith(IJsonFileRetrievalService jsonFileRetrievalService)
        {
            this.JsonFileRetrievalService = jsonFileRetrievalService;
            return this;
        }

        public LocalJsonFileRatesRepositoryMockBuilder BuildWith(IJsonFileParserService jsonFileParserService)
        {
            this.JsonFileParserService = jsonFileParserService;
            return this;
        }

        public RatesRepository Build()
        {
            return new RatesRepository(JsonFileRetrievalService, JsonFileParserService);
        }

    }
}
