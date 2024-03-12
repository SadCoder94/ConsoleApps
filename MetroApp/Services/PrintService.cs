using MetroApp.Models;
using System;

namespace MetroApp.Services
{
    public interface IPrintService
    {
        void PrintSummary(Station station);
    }

    public class PrintService : IPrintService
    {
        private readonly IStationService _stationService;
        public PrintService(IStationService stationService)
        {
            _stationService = stationService;
        }

        public void PrintSummary(Station station)
        {
            var centralStationCollection = _stationService.GetStationSummary(station);
            Console.WriteLine($"TOTAL_COLLECTION CENTRAL {centralStationCollection.StationCollection} {centralStationCollection.TotalDiscount}");
            Console.WriteLine($"PASSENGER_TYPE_SUMMARY");

            var stationPassengerSummary = centralStationCollection.passengersOutbound;

            foreach (var pair in stationPassengerSummary)
                Console.WriteLine($"{(pair.Key)} {pair.Value}");
        }
    }
}
