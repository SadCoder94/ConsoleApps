using MetroApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.Models
{
    public class JourneyInfo
    {
        public Station DestinationStation { get; set; }
        public Station StartingStation { get; set; }
        public PassengerType PassengerType { get; set; }
        public int ServiceCharges { get; set; }
        public int Discount { get; set; }
        public int Charges { get; set; }
        public string CardNo { get; set; }
    }
}
