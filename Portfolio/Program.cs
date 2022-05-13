using Portfolio.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Portfolio
{
    class Program
    {
        static void Main(string[] args)
        {
            String filePath = "";// args[0];
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
                                funds.Add(fundhandler.GetFundInfo(f));

                            user.AddFundsToPortFolio(funds);

                            break;
                        case "CALCULATE_OVERLAP":
                            var fundNameToOverLapAgainst = command[1];
                            var fundToOverLapAgainst = fundhandler.GetFundInfo(command[1]); 
                            
                            var userPortfolio = user.GetPortfolio();

                            var overlapCalculations = fundhandler.GetOverLap(fundToOverLapAgainst,userPortfolio);

                            foreach (var calc in overlapCalculations)
                                Console.WriteLine(calc);

                            break;
                        case "ADD_STOCK":
                            var fundName = command[1];
                            var stockNameAsArr = command.Skip(2).ToArray();
                            string stockName = "";

                            if (stockNameAsArr.Length > 1)
                                stockName = string.Join("", stockNameAsArr);
                            else
                                stockName = stockNameAsArr[0];

                            fundhandler.AddStock(fundName, stockName);
                            break;
                    }
                }
            }
        }
    }
}
