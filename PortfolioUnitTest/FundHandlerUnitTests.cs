using Portfolio.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PortfolioUnitTest
{
    public class FundHandlerUnitTests
    {
        [Fact]
        public void TestGetFundInfo()
        {
            var fundHandler = new FundHandler();

            var fundInfo = fundHandler.GetFundInfo("ICICI_PRU_NIFTY_NEXT_50_INDEX");

            Assert.NotNull(fundInfo);
        }

        [Fact]
        public void TestOverlapCalculation()
        {
            var fundHandler = new FundHandler();

            var fundToClaculateAgainst = fundHandler.GetFundInfo("MIRAE_ASSET_EMERGING_BLUECHIP");

            var portfolioList = new List<Fund> { 
                fundHandler.GetFundInfo("AXIS_BLUECHIP"),
                fundHandler.GetFundInfo("ICICI_PRU_BLUECHIP"),
                fundHandler.GetFundInfo("UTI_NIFTY_INDEX")
            };

            var portfolioCalc = fundHandler.GetOverLap(fundToClaculateAgainst, portfolioList);

            Assert.NotNull(portfolioCalc);
            Assert.Equal(3, portfolioCalc.Count);
            
            Assert.Equal(39.13, portfolioCalc[0].Item3);
            Assert.Equal(38.10, portfolioCalc[1].Item3);
            Assert.Equal(65.52, portfolioCalc[2].Item3);
        }

        [Fact]
        public void TestOverlapCalculationAfterAddStock()
        {
            var fundHandler = new FundHandler();

            var fundToClaculateAgainst = fundHandler.GetFundInfo("MIRAE_ASSET_EMERGING_BLUECHIP");

            var fundToAddStocks = fundHandler.GetFundInfo("AXIS_BLUECHIP");
            fundHandler.AddStock(fundToAddStocks, "TCS");

            var portfolioList = new List<Fund> {
                fundHandler.GetFundInfo("AXIS_BLUECHIP")
            };

            var portfolioCalc = fundHandler.GetOverLap(fundToClaculateAgainst, portfolioList);

            Assert.NotNull(portfolioCalc);
            Assert.Single(portfolioCalc);

            Assert.Equal(38.71, portfolioCalc[0].Item3);
        }

    }
}
