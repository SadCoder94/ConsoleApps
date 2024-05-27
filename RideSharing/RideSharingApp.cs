using RideSharing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing
{
    internal class RideSharingApp
    {
        readonly IUserService _userService;
        readonly IRideService _rideService;
        readonly IDisplayService _displayService;
        public RideSharingApp(IUserService userService, IRideService rideService, IDisplayService displayService)
        {
            _userService = userService;
            _rideService = rideService;
            _displayService = displayService;
        }

        internal void Run(List<string[]> cmdList)
        {
            foreach (var cmd in cmdList)
            {
                switch (cmd[0])
                {
                    case "ADD_DRIVER":
                        _userService.AddUser(Models.UserTypeEnum.DRIVER, cmd[1], int.Parse(cmd[2]), int.Parse(cmd[3]));
                        break;
                    case "ADD_RIDER":
                        _userService.AddUser(Models.UserTypeEnum.RIDER, cmd[1], int.Parse(cmd[2]), int.Parse(cmd[3]));
                        break;
                    case "MATCH":
                        var matches = _rideService.FindMatches(cmd[1]);
                        _displayService.DisplayDrivers(matches);
                        break;
                    case "START_RIDE":
                        var status = _rideService.StartRide(cmd[1], int.Parse(cmd[2]), cmd[3]);
                        _displayService.DisplayRideStatus(status, cmd[1]);
                        break;
                    case "STOP_RIDE":
                        status = _rideService.StopRide(cmd[1], int.Parse(cmd[2]), int.Parse(cmd[3]), int.Parse(cmd[4]));
                        _displayService.DisplayRideStatus(status, cmd[1]);
                        break;
                    case "BILL":
                        var rideId = cmd[1];
                        _displayService.DisplayBill(rideId);
                        break;
                }
            }
            
        }
    }
}
