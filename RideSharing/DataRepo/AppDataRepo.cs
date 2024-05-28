using RideSharing.Models;
using System.Collections.Generic;
using System.Linq;

namespace RideSharing.DataRepo
{
    public interface IAppDataRepo
    {
        void Add(User user);
        bool AddRide(Ride ride);
        Ride GetRideById(string id);
        User GetUserById(string userId);
        List<User> GetUserByType(UserTypeEnum driver);
        bool IsDriverInRide(string driverId);
        void UpdateRideDetails(Ride ride);
        void UpdateUser(User user);
    }
    public class AppDataRepo : IAppDataRepo
    {
        private List<User> UserList;
        private List<Ride> Rides;
        public AppDataRepo()
        {
            UserList = new List<User>();
            Rides = new List<Ride>();
        }

        public User GetUserById(string userId) => UserList.Find(x => x.UserId == userId);

        public List<User> GetUserByType(UserTypeEnum driver) => UserList.Where(x => x.UserType == driver).ToList();

        void IAppDataRepo.Add(User user) => UserList.Add(user);

        public Ride GetRideById(string id) => Rides.Where(x => x.RideId == id).FirstOrDefault();

        public bool AddRide(Ride ride)
        {
            Rides.Add(ride);
            return true;
        }

        public void UpdateRideDetails(Ride ride)
        {
            var rideInfo = GetRideById(ride.RideId);
            if (rideInfo != null)
            {
                rideInfo.Amount = ride.Amount;
            }

        }

        public bool IsDriverInRide(string driverId)
        {
            var rideByDriver = Rides.Where(x => x.DriverId == driverId && !x.RideIsComplete()).ToList();
            return rideByDriver.Any();
        }

        public void UpdateUser(User user)
        {
            var userInList = UserList.FirstOrDefault(x => x.UserId == user.UserId);

            if(userInList != null)
            {
                var indexOfUser = UserList.IndexOf(userInList);
                if (indexOfUser != -1)
                {
                    UserList[indexOfUser] = user;
                }

            }
        }
    }
}
