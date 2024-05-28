using RideSharing.DataRepo;
using RideSharing.Models;
using RideSharing.SharedMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RideSharing.Services
{
    public interface IUserService
    {
        void AddUser(UserTypeEnum userType, string userId, int xCoord, int yCoord);
        List<User> GetDriversInRange(int xCoord, int yCoord, int? upto=null);
        User GetUserById(string userId);

        void UpdateUser(User user);
    }

    public class UserService : IUserService
    {
        IAppDataRepo _dataRepo;
        public UserService(IAppDataRepo dataRepo)
        {
            _dataRepo = dataRepo;
        }

        public void AddUser(UserTypeEnum userType, string userId, int xCoord, int yCoord)
        {
            var user = new User
            {
                UserType = userType,
                UserId = userId,
                xCoord = xCoord,
                yCoord = yCoord
            };

            _dataRepo.Add(user);
        }

        public List<User> GetDriversInRange(int xCoord,int yCoord, int? upto=null)
        {
            var allDrivers = _dataRepo.GetUserByType(UserTypeEnum.DRIVER);

            var driversInRange = new List<object>();
                
            foreach(var driver in allDrivers)
            {
                if (!_dataRepo.IsDriverInRide(driver.UserId))
                {
                    var distanceFromRider = CalculationMethods.GetEucledianDistance(xCoord, yCoord, driver.xCoord, driver.yCoord);

                    if (distanceFromRider <= 5)
                        driversInRange.Add(new { driver, distanceFromRider });
                }
            }

            var driversInDescendingOrder = driversInRange.Cast<dynamic>().OrderBy(x => x.distanceFromRider).ThenBy(x => x.driver.UserId).Select(x => x.driver).Cast<User>();

            if (upto.HasValue)
                driversInDescendingOrder.Take(upto.Value);
            
            return driversInDescendingOrder.ToList();
        }

        public User GetUserById(string userId)
        {
            var user = _dataRepo.GetUserById(userId);
            return user;
        }

        public void UpdateUser(User user)
        {
            _dataRepo.UpdateUser(user);
        }
    }
}
