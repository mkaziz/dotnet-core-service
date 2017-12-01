using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SpotHero.Services.BusObj.Models.Server;
using SpotHero.Services.BusObj.Repositories;
using SpotHero.Services.BusObj.Services;
using System;
using System.Collections.Generic;

namespace SpotHero.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {




        }

        [TestMethod]
        public void GetJsonRates_RatesWithinOnePeriod_SuccessfullyRetrievesRate()
        {
            //Arrange
            var jsonFileRetrievalService = Substitute.For<IJsonFileRetrievalService>();
            var jsonFileParserService = Substitute.For<IJsonFileParserService>();

            jsonFileParserService.GetRatesFromJson(Arg.Any<string>()).Returns(new List<RatesForDay>
            {
                new RatesForDay
                {
                    Day = DayOfWeek.Monday,
                    Rates = new List<RateForTimePeriod>
                    {
                       new RateForTimePeriod
                       {
                           StartTime = DateTime.Parse("Nov 27 2017, 4PM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 6PM"),
                           Price = 1500
                       },
                    }
                }
            });

            //Act
            var ratesRepo = new LocalJsonFileRatesRepository(jsonFileRetrievalService, jsonFileParserService);
            var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 4:10PM"), DateTime.Parse("Nov 27 2017, 4:50PM"));

            //Assert
            Assert.IsTrue(result.Price == 1500);
        }
    }
}
