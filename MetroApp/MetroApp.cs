using MetroApp.Models;
using MetroApp.Services;
using System;
using System.Collections.Generic;

namespace MetroApp
{
    internal partial class Program
    {
        public class MetroApp
        {
            private readonly ICardService _cardService;
            private readonly ICheckInService _checkInService;
            private readonly IStationService _stationService;
            private readonly IPrintService _printService;

            public MetroApp(ICardService cardService, ICheckInService checkInService, IStationService stationService, IPrintService printService)
            {
                _cardService = cardService;
                _checkInService = checkInService;
                _stationService = stationService;
                _printService = printService;
            }

            public void Run(List<string[]> commands)
            {
                foreach (var command in commands)
                {
                    switch (command[0])
                    {
                        case "BALANCE":
                            _cardService.SetCardInfo(command[1], int.Parse(command[2]));
                            break;
                        case "CHECK_IN":
                            var passengertype = (PassengerType)Enum.Parse(typeof(PassengerType), command[2]);
                            var station = (Station)Enum.Parse(typeof(Station), command[3]);
                            var cardNo = command[1];

                            _checkInService.CheckIn(cardNo, passengertype, station);
                            break;
                        case "PRINT_SUMMARY":
                            _printService.PrintSummary(Station.CENTRAL);
                            _printService.PrintSummary(Station.AIRPORT);
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}
