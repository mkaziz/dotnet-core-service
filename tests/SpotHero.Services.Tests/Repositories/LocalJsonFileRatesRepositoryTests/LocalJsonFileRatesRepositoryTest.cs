using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SpotHero.Services.BusObj.Models.Server;
using SpotHero.Services.BusObj.Repositories;
using SpotHero.Services.BusObj.Services;
using System;
using System.Collections.Generic;

namespace SpotHero.Services.Tests.Repositories.LocalJsonFileRatesRepositoryTests
{
    [TestClass]
    public class LocalJsonFileRatesRepositoryTest
    {
        [TestMethod]
        public void GetJsonRates_RatesWithinOnePeriod_SuccessfullyRetrievesRate()
        {
            //Arrange
            var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().Build();

            //Act
            var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 4:10PM"), DateTime.Parse("Nov 27 2017, 4:50PM"));

            //Assert
            Assert.IsTrue(result.Price == 2500);
        }
    }
}
