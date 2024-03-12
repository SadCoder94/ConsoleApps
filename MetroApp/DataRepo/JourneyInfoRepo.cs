using MetroApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroApp.DataRepo
{
    public interface IJourneyInfoRepo
    {
        void AddJourney(JourneyInfo journey);
        StationSummary GetStationSummary(Station station);
        bool IsRoundtrip(JourneyInfo journey);
    }

    public class JourneyInfoRepo : IJourneyInfoRepo
    {
        private List<JourneyInfo> _journeys;
        public JourneyInfoRepo()
        {
            _journeys = new List<JourneyInfo>();
        }

        public StationSummary GetStationSummary(Station station)
        {
            var infoForStartStation = _journeys.Where(x => x.StartingStation == station).ToList();

            var outboundPassengers = infoForStartStation.Select(x => x.PassengerType).ToList();
            var outboundDictionary = outboundPassengers.GroupBy(x => x)
                                .ToDictionary(g => g.Key, g => g.Count())
                                .OrderBy(x => x.Key.ToString())
                                .ThenByDescending(x => x.Value)
                                .ToDictionary(x => x.Key, x => x.Value);

            var totalCharges = infoForStartStation.Sum(x => x.Charges);
            var totalServiceCharges = infoForStartStation.Sum(x => x.ServiceCharges);
            var totalDiscounts = infoForStartStation.Sum(x => x.Discount);

            var stationSummary = new StationSummary
            {
                station = station,
                passengersOutbound = outboundDictionary,
                TotalDiscount = totalDiscounts,
                StationCollection = totalCharges + totalServiceCharges
            };

            return stationSummary;
        }

        /// <summary>
        /// Adds journey to repo
        /// </summary>
        public void AddJourney(JourneyInfo journey)
        {
            _journeys.Add(journey);
        }

        private List<JourneyInfo> GetOppositeTrips(JourneyInfo journey)
        {
            var oppTrips = _journeys.Where(x =>
            x.StartingStation == journey.DestinationStation
            && x.DestinationStation == journey.StartingStation
            && x.PassengerType == journey.PassengerType
            && x.CardNo == journey.CardNo).ToList();

            return oppTrips;
        }
        private List<JourneyInfo> GetSimilarTrips(JourneyInfo journey)
        {
            var simTrips = _journeys.Where(x =>
            x.StartingStation == journey.StartingStation
            && x.DestinationStation == journey.DestinationStation
            && x.PassengerType == journey.PassengerType
            && x.CardNo == journey.CardNo).ToList();

            return simTrips;
        }

        public bool IsRoundtrip(JourneyInfo journey)
        {
            var sameTrips = GetSimilarTrips(journey);

            var oppTrips = GetOppositeTrips(journey);

            if (Math.Abs(sameTrips.Count - oppTrips.Count) == 0)//If both are equal, a roundtrip is already made
                return false;

            return true;
        }
    }
}
