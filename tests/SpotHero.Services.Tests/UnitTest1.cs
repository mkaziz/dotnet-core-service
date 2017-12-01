using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SpotHero.Services.BusObj.Repositories;
using SpotHero.Services.BusObj.Services;

namespace SpotHero.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var jsonFileService = Substitute.For<IJsonFileRetrievalService>();
        }
    }
}
