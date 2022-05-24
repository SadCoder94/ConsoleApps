using Portfolio.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Portfolio
{
    class Program
    {
        const string FUND_NOT_FOUND = "FUND_NOT_FOUND";
        static void Main(string[] args)
        {
            String filePath = args[0];
            var fundhandler = new FundHandler();
            var user = new User();

            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (var commandString in File.ReadLines(@$"{filePath}"))
                {
                    string[] command = commandString.Split(' ');
                    switch (command[0])
                    {
                        case "CURRENT_PORTFOLIO":
                            var fundNamesToAdd = command.Skip(1).ToList();
                            var funds = new List<Fund>();

                            foreach(var f in fundNamesToAdd)
                            {
                                var fundInfo = fundhandler.GetFundInfo(f);
                                if(fundInfo != null)
                                    funds.Add(fundInfo);
                            }
                                
                            user.AddFundsToPortFolio(funds);

                            break;

                        case "CALCULATE_OVERLAP":
                            var fundToOverLapAgainst = fundhandler.GetFundInfo(command[1]);
                            if (fundToOverLapAgainst == null)
                            {
                                Console.WriteLine(FUND_NOT_FOUND);
                                break;
                            }

                            var userPortfolio = user.GetPortfolio();

                            var overlapCalculations = fundhandler.GetOverLap(fundToOverLapAgainst,userPortfolio);

                            foreach (var calc in overlapCalculations)
                                if(calc.Item3 > 0.00)
                                Console.WriteLine(calc.Item1+" "+calc.Item2+" "+calc.Item3.ToString("0.00")+"%");

                            break;

                        case "ADD_STOCK":
                            var fundName = command[1];
                            var fundByFundName = fundhandler.GetFundInfo(fundName);
                            if (fundByFundName == null)
                            {
                                Console.WriteLine(FUND_NOT_FOUND);
                                break;
                            }

                            var stockNameAsArr = command.Skip(2).ToArray();
                            string stockName = "";

                            if (stockNameAsArr.Length > 1)
                                stockName = string.Join(" ", stockNameAsArr);
                            else
                                stockName = stockNameAsArr[0];

                            fundhandler.AddStock(fundByFundName, stockName);
                            break;
                    }
                }
            }
        }
    }
}
