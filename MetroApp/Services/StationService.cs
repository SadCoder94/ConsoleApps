using MetroApp.DataRepo;
using MetroApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.Services
{
    public interface IStationService
    {
        StationSummary GetStationSummary(Station station);
    }

    public class StationService : IStationService
    {
        private readonly IJourneyInfoRepo _journeyInfoRepo;
        public StationService(IJourneyInfoRepo journeyInfoRepo)
        {
            _journeyInfoRepo = journeyInfoRepo;
        }

        public StationSummary GetStationSummary(Station station)
        {
            var stationSummary = _journeyInfoRepo.GetStationSummary(station);
            return stationSummary;
        }

    }

}
