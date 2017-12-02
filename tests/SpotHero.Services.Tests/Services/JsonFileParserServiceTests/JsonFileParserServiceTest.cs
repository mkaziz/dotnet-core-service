using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotHero.Services.BusObj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotHero.Services.Tests.Services.JsonFileParserServiceTests
{
    [TestClass]
    public class JsonFileParserServiceTest
    {
        [TestMethod]
        public void GetRatesFromJson_EmptyString_ReturnsEmptyList()
        {
            //Arrange
            var service = new JsonFileParserService();

            //Act
            var rates = service.GetRatesFromJson(string.Empty);

            //Assert
            Assert.IsTrue(!rates.Any());
        }

        [TestMethod]
        public void GetRatesFromJson_OneRateForSeveralDays_ReturnsListWithThatManyRates()
        {
            //Arrange
            var service = new JsonFileParserService();

            //Act
            var rates = service.GetRatesFromJson(@"{
                  ""rates"": [
                    {
                        ""days"": ""mon,tues,wed,thurs,fri"",
                        ""times"": ""0600-1800"",
                        ""price"": 1500
                    }
                  ]
                }");

            //Assert
            Assert.IsTrue(rates.Count == 5);
            Assert.IsTrue(rates.First().Rates.First().Price == 1500);
        }

        [TestMethod]
        public void GetRatesFromJson_MultiplesDaysClientRate_ReturnsOnlyNumberOfDays()
        {
            //Arrange
            var service = new JsonFileParserService();

            //Act
            var rates = service.GetRatesFromJson(@"{
                  ""rates"": [
                    {
                        ""days"": ""mon,tues,wed,thurs,fri"",
                        ""times"": ""0600-1800"",
                        ""price"": 1500
                    },
                    {
                        ""days"": ""thurs,fri,sat,sun"",
                        ""times"": ""0100-1200"",
                        ""price"": 400
                    }
                  ]
                }");

            //Assert
            Assert.IsTrue(rates.Count == 7);
            Assert.IsTrue(rates.FirstOrDefault(d => DayOfWeek.Sunday == d.Day).Rates.First().Price == 400);
        }

        [TestMethod]
        public void GetRatesFromJson_MultiplesDaysClientRate_ReturnsCorrectNumberOfRatesForDays()
        {
            //Arrange
            var service = new JsonFileParserService();

            //Act
            var rates = service.GetRatesFromJson(@"{
                  ""rates"": [
                    {
                        ""days"": ""mon,tues,wed,thurs,fri"",
                        ""times"": ""0600-1800"",
                        ""price"": 1500
                    },
                    {
                        ""days"": ""thurs,fri,sat,sun"",
                        ""times"": ""0100-1200"",
                        ""price"": 400
                    }
                  ]
                }");

            //Assert
            Assert.IsTrue(rates.FirstOrDefault(d => DayOfWeek.Sunday == d.Day).Rates.Count == 1);
            Assert.IsTrue(rates.FirstOrDefault(d => DayOfWeek.Thursday == d.Day).Rates.Count == 2);
        }
    }
}
