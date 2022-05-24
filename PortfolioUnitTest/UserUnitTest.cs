using Portfolio.Classes;
using System;
using System.Collections.Generic;
using Xunit;

namespace PortfolioUnitTest
{
    public class UserUnitTest
    {
        [Fact]
        public void AddUserFunds()
        {
            var user = new User();
            
            var fundsToAdd = new List<Fund>();
            fundsToAdd.Add(new Fund { name = "new fund 1", stocks = new List<string> { "share 1", "share 2"} });
            fundsToAdd.Add(new Fund { name = "new fund 2", stocks = new List<string> { "share 2", "share 3"} });

            user.AddFundsToPortFolio(fundsToAdd);

            var userPortfolio = user.GetPortfolio();

            Assert.Equal(userPortfolio, fundsToAdd);
        }
    }
}
