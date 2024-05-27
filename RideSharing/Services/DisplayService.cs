using RideSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Services
{
    public interface IDisplayService
    {
        void DisplayBill(string rideId);
        void DisplayDrivers(List<string> driverNames);
        void DisplayRideStatus(RideStatus rideStatus, string rideid);
    }

    public class DisplayService : IDisplayService
    {
        private readonly IRideService _rideService;
        public DisplayService(IRideService rideService) 
        {
            _rideService = rideService;
        }

        public void DisplayBill(string rideId)
        {
            var rideInfo = _rideService.GetRideInfoByRideId(rideId);
            if (rideInfo == null)
            {
                Console.WriteLine("INVALID_RIDE");
                return;
            }

            if(!rideInfo.RideIsComplete())//ride not complete
            {
                Console.WriteLine("RIDE_NOT_COMPLETED");
                return;
            }

            Console.WriteLine($"BILL {rideId} {rideInfo.DriverId} {rideInfo.Amount:0.00}");

        }

        public void DisplayDrivers(List<string> driverNames)
        {
            if (driverNames.Count == 0)
            {
                Console.WriteLine("NO_DRIVERS_AVAILABLE");
                return;
            }

            Console.WriteLine($"DRIVERS_MATCHED {string.Join(" ", driverNames)}");
        }

        public void DisplayRideStatus(RideStatus rideStatus, string rideId)
        {
            switch(rideStatus)
            {
                case RideStatus.INVALID: Console.WriteLine("INVALID_RIDE");
                    return;
                case RideStatus.STARTED:
                    Console.WriteLine($"RIDE_STARTED {rideId}");
                    return;
                case RideStatus.STOPPED:
                    Console.WriteLine($"RIDE_STOPPED {rideId}");
                    return;
            }
        }
    }
}
