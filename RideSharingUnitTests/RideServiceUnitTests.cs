using Moq;
using RideSharing.DataRepo;
using RideSharing.Models;
using RideSharing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RideSharingUnitTests
{
    public class RideServiceUnitTests
    {
        private Mock<IUserService> mockUserService;
        private Mock<IAppDataRepo> mockAppDataRepo;
        private IRideService rideService;
        public RideServiceUnitTests()
        {
            mockUserService = new Mock<IUserService>();
            mockAppDataRepo = new Mock<IAppDataRepo>();
            rideService = new RideService(mockUserService.Object, mockAppDataRepo.Object);
        }

        [Fact]
        public List<string> GetDriverListForUserIfDriversExist()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0});
            var driversList = new List<User> 
            { 
                new User { UserId = "ID2", UserType = UserTypeEnum.DRIVER, xCoord = 3, yCoord = 4 },
                new User { UserId = "ID3", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 3 },
                new User { UserId = "ID4", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 4 }
            };
            mockUserService.Setup(d => d.GetDriversInRange(0, 0, 5)).Returns(driversList);


            //execute
            var drivers = rideService.FindMatches("ID1");

            //assert
            mockUserService.Verify(d => d.GetUserById("ID1"), Times.Once);
            mockUserService.Verify(d => d.GetDriversInRange(0, 0, 5), Times.Once);

            Assert.NotEmpty(drivers);
            Assert.Equal(3,drivers.Count);
            
            return drivers;
        }

        [Fact]
        public void GetDriverListForUserIfDriversdDontExist()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 });
            var driversList = new List<User>();
            mockUserService.Setup(d => d.GetDriversInRange(0, 0, 5)).Returns(driversList);


            //execute
            var drivers = rideService.FindMatches("ID1");

            //assert
            mockUserService.Verify(d => d.GetUserById("ID1"), Times.Once);
            mockUserService.Verify(d => d.GetDriversInRange(0, 0, 5), Times.Once);

            Assert.Empty(drivers);
        }

        [Fact]
        public void StartRideIfRideAlreadyExists()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0});
            var driversList = new List<User>
            {
                new User { UserId = "ID2", UserType = UserTypeEnum.DRIVER, xCoord = 3, yCoord = 4 },
                new User { UserId = "ID3", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 3 },
                new User { UserId = "ID4", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 4 }
            };
            mockUserService.Setup(d => d.GetDriversInRange(0, 0, 5)).Returns(driversList);
            mockAppDataRepo.Setup(x => x.GetRideById("R1")).Returns(new Ride { RideId = "R1" });

            //execute
            var response = rideService.StartRide("R1",3,"D1");

            //
            Assert.Equal(RideStatus.INVALID,response);
        }

        [Fact]
        public void StartRideIfRideIfNotExists()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(
                new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 }
                );
            var driversList = new List<User>
            {
                new User { UserId = "ID2", UserType = UserTypeEnum.DRIVER, xCoord = 3, yCoord = 4 },
                new User { UserId = "ID3", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 3 },
                new User { UserId = "ID4", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 4 }
            };
            mockUserService.Setup(d => d.GetDriversInRange(0, 0, 5)).Returns(driversList);
            mockAppDataRepo.Setup(x => x.GetRideById("R1")).Returns((Ride) null);

            //execute
            var response = rideService.StartRide("R1", 3, "ID1");

            //
            Assert.Equal(RideStatus.STARTED, response);
        }

        [Fact]
        public void StartRideIfRideIfNoDriversAvailable()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(
                new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 }
                );

            var driversList = new List<User>();

            mockUserService.Setup(d => d.GetDriversInRange(0, 0, 5)).Returns(driversList);
            mockAppDataRepo.Setup(x => x.GetRideById("R1")).Returns((Ride)null);

            //execute
            var response = rideService.StartRide("R1", 3, "ID1");

            //
            Assert.Equal(RideStatus.INVALID, response);
        }

        [Fact]
        public void StopRideIfRideAlreadyExists()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 });
            mockAppDataRepo.Setup(x => x.GetRideById("R1")).Returns(new Ride { RideId = "R1", StartX = 0, StartY = 0, DriverId = "DR1", RiderId = "ID1" });

            //execute
            var response = rideService.StopRide("R1", 9,9, 10);

            //assert
            Assert.Equal(RideStatus.STOPPED, response);
            mockAppDataRepo.Verify(x => x.UpdateRideDetails(It.IsAny<Ride>()), Times.Once);
        }

        [Fact]
        public void StopRideIfRideAlreadyEnded()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 });
            mockAppDataRepo.Setup(x => x.GetRideById("R1")).Returns(new Ride { RideId = "R1", StartX = 0, StartY = 0, DriverId = "DR1", RiderId = "ID1", Amount = 231, StopX = 9, StopY = 9 });

            //execute
            var response = rideService.StopRide("R1", 9, 9, 10);

            //assert
            Assert.Equal(RideStatus.INVALID, response);
            mockAppDataRepo.Verify(x => x.UpdateRideDetails(It.IsAny<Ride>()), Times.Never);
        }

        [Fact]
        public void StopRideIfRideIfNotExists()
        {
            //setup
            mockUserService.Setup(d => d.GetUserById("ID1")).Returns(
                new User { UserId = "ID1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 }
                );
            
            mockAppDataRepo.Setup(x => x.GetRideById("R1")).Returns((Ride)null);

            //execute
            var response = rideService.StopRide("R1", 3,3,3);

            //
            Assert.Equal(RideStatus.INVALID, response);
        }
    }
}
