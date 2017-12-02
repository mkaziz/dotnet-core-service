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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetJsonRates_EndTimeBeforeStartTime_ThrowsException()
        {
            //Arrange
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
                           Price = 2500
                       },
                    }
                }
            });

            var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().BuildWith(jsonFileParserService).Build();

            //Act
            var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 4:50PM"), DateTime.Parse("Nov 27 2017, 4:10PM"));

            //Assert

        }

        [TestMethod]
        public void GetJsonRates_GarageClosedOnDay_ExpectedResult()
        {
            //Arrange
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
                           Price = 2500
                       },
                    }
                }
            });

            var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().BuildWith(jsonFileParserService).Build();

            //Act
            var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 28 2017, 4:00PM"), DateTime.Parse("Nov 28 2017, 4:10PM"));

            //Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void GetJsonRates_RatesWithinOnePeriod_SuccessfullyRetrievesRate()
        {
            //Arrange
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
                           Price = 2500
                       },
                    }
                }
            });

            var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().BuildWith(jsonFileParserService).Build();

            //Act
            var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 4:10PM"), DateTime.Parse("Nov 27 2017, 4:50PM"));

            //Assert
            Assert.IsTrue(result.Price == 2500);
        }

        [TestMethod]
        public void GetJsonRates_RatesForDisjointBlocks_ReturnsNull()
        {
            //Arrange
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
                           StartTime = DateTime.Parse("Nov 27 2017, 2PM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 3PM"),
                           Price = 1500
                       },
                       new RateForTimePeriod
                       {
                           StartTime = DateTime.Parse("Nov 27 2017, 4PM"),
                           EndTime = DateTime.Parse("Nov 27 2017, 6PM"),
                           Price = 2500
                       },
                    }
                }
            });

            var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().BuildWith(jsonFileParserService).Build();

            //Act
            var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 2:10PM"), DateTime.Parse("Nov 27 2017, 4:50PM"));

            //Assert
            Assert.IsNull(result);
        }

        //[TestMethod]
        //public void GetJsonRates_RatesForTwoConnectedButDifferentBlocks_ReturnsHigherRate()
        //{
        //    var jsonFileParserService = Substitute.For<IJsonFileParserService>();
        //    jsonFileParserService.GetRatesFromJson(Arg.Any<string>()).Returns(new List<RatesForDay>
        //    {
        //        new RatesForDay
        //        {
        //            Day = DayOfWeek.Monday,
        //            Rates = new List<RateForTimePeriod>
        //            {
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 2PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 4PM"),
        //                   Price = 1500
        //               },
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 4PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 6PM"),
        //                   Price = 2500
        //               },
        //            }
        //        }
        //    });

        //    var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().BuildWith(jsonFileParserService).Build();

        //    //Act
        //    var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 2:10PM"), DateTime.Parse("Nov 27 2017, 4:50PM"));

        //    //Assert
        //    Assert.IsTrue(result.Price == 2500);

        //}

        //[TestMethod]
        //public void GetJsonRates_RatesForManyConnectedButDifferentBlocks_ReturnsHigherRate()
        //{
        //    var jsonFileParserService = Substitute.For<IJsonFileParserService>();
        //    jsonFileParserService.GetRatesFromJson(Arg.Any<string>()).Returns(new List<RatesForDay>
        //    {
        //        new RatesForDay
        //        {
        //            Day = DayOfWeek.Monday,
        //            Rates = new List<RateForTimePeriod>
        //            {
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 2PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 3PM"),
        //                   Price = 1500
        //               },
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 3PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 4PM"),
        //                   Price = 2500
        //               },
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 4PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 5PM"),
        //                   Price = 3500
        //               },
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 5PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 6PM"),
        //                   Price = 1500
        //               },
        //               new RateForTimePeriod
        //               {
        //                   StartTime = DateTime.Parse("Nov 27 2017, 6PM"),
        //                   EndTime = DateTime.Parse("Nov 27 2017, 7PM"),
        //                   Price = 6500
        //               },
        //            }
        //        }
        //    });

        //    var ratesRepo = new LocalJsonFileRatesRepositoryMockBuilder().BuildWith(jsonFileParserService).Build();

        //    //Act
        //    var result = ratesRepo.GetRateForTimePeriod(DateTime.Parse("Nov 27 2017, 3:10PM"), DateTime.Parse("Nov 27 2017, 5:50PM"));

        //    //Assert
        //    Assert.IsTrue(result.Price == 3500);

        //}
    }
}
