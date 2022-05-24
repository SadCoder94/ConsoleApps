using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace Portfolio.Classes
{
    public class FundHandler
    {
        List<Fund> funds;
        public FundHandler()
        {
            string json = File.ReadAllText("ExistingMarketData.json");
            var jsonFundData = JsonConvert.DeserializeObject<Funds>(json);
            funds = jsonFundData.funds;
        }

        public Fund GetFundInfo(string fundName)
        {
            var fundWithName = funds.Where(f => f.name == fundName);
            if (fundWithName.Count() == 1)//single instance of the fund name exists
            {
                return fundWithName.First();
            }
            else
                return null;
        }

        public List<(string, string, double)> GetOverLap(Fund forFund, List<Fund> portfolio)
        {
            var overlaps = new List<(string, string, double)>();
            foreach (var fund in portfolio)
            {
                double totalStocks = (double)fund.stocks.Count() + (double)forFund.stocks.Count();

                var commonStocks = fund.stocks.Intersect(forFund.stocks).ToList();

                double overLap = ((2 * (double)commonStocks.Count()) / totalStocks) * 100;

                overlaps.Add((forFund.name, fund.name, Math.Round(overLap, 2)));
            }

            return overlaps;
        }

        public void AddStock(Fund fund, string stockName)
        {
            if (fund != null && stockName != "")
            {
                fund.stocks.Add(stockName);
            }
        }
    }
}