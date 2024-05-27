using RideSharing.DataRepo;
using RideSharing.Models;
using RideSharing.SharedMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RideSharing.Services
{
    public interface IRideService
    {
        List<string> FindMatches(string userId);
        Ride GetRideInfoByRideId(string rideId);
        RideStatus StartRide(string rideId, int N, string riderID);
        RideStatus StopRide(string rideId, int destX, int destY, int timeTakenInMin);
    }

    public class RideService : IRideService
    {
        readonly IUserService _userService;
        readonly IAppDataRepo _appDataRepo;
        public RideService(IUserService userService, IAppDataRepo appDataRepo)
        {
            _userService = userService;
            _appDataRepo = appDataRepo;
        }

        public List<string> FindMatches(string userId)
        {
            var user = _userService.GetUserById(userId);
            var drivers = _userService.GetDriversInRange(user.xCoord, user.yCoord,5);

            return drivers.Select(x => x.UserId).ToList();
        }

        public RideStatus StartRide(string rideId, int N, string riderID)
        {
            var rideExists = _appDataRepo.GetRideById(rideId) != null;
            if (rideExists)
                return RideStatus.INVALID;

            var getDriversinRadiusofUser = FindMatches(riderID);
            if (!getDriversinRadiusofUser.Any())
                return RideStatus.INVALID;
            
            var getNthDriver = getDriversinRadiusofUser[N-1];

            var rideAdd = _appDataRepo.AddRide(new Ride { RideId = rideId, RiderId = riderID, DriverId = getNthDriver});

            return RideStatus.STARTED;
        }

        public RideStatus StopRide(string rideId, int destX, int destY, int timeTakenInMin)
        {
            var rideInfo = _appDataRepo.GetRideById(rideId);
            if (rideInfo == null || rideInfo.RideIsComplete())
                return RideStatus.INVALID;

            var riderInfo = _userService.GetUserById(rideInfo.RiderId);

            var distanceTravelled = CalculationMethods.GetEucledianDistance(riderInfo.xCoord,riderInfo.yCoord, destX, destY);

            var amount = CalculationMethods.CalculateRideAmount(timeTakenInMin, distanceTravelled);

            rideInfo.Amount = amount;
            rideInfo.StopX = destX;
            rideInfo.StopY = destY;
            
            _appDataRepo.UpdateRideDetails(rideInfo);

            return RideStatus.STOPPED;
        }

        public Ride GetRideInfoByRideId(string rideId)
        {
            return _appDataRepo.GetRideById(rideId);
        }

    }
}
