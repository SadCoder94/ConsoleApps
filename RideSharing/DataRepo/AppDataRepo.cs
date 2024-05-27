﻿using RideSharing.Models;
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
        void UpdateRideDetails(Ride ride);
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
    }
}
