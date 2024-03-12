using MetroApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.Models
{
    public class StationSummary
    {
        public Station station { get; set; }
        public int StationCollection { get; set; }
        public Dictionary<PassengerType, int> passengersOutbound { get; set; }
        public object TotalDiscount { get; internal set; }

        public StationSummary(Station station)
        {
            this.station = station;
            passengersOutbound = Enum.GetValues(typeof(PassengerType))
                                   .Cast<PassengerType>()
                                   .ToDictionary(passenger => passenger, passenger => 0);
        }

        public StationSummary()
        {
                
        }
    }
}
