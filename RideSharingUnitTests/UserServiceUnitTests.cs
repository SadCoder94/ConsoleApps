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
    public class UserServiceUnitTests
    {
        private Mock<IAppDataRepo> mockDataRepo;
        private IUserService userService;
        public UserServiceUnitTests()
        {
            mockDataRepo = new Mock<IAppDataRepo>();
            userService = new UserService(mockDataRepo.Object);
        }

        [Fact]
        public void ReturnUser()
        {
            //setup
            var mockUser = new User { UserId = "DI1", UserType = UserTypeEnum.RIDER, xCoord = 0, yCoord = 0 };
            mockDataRepo.Setup(x => x.GetUserById("DI1")).Returns(mockUser);

            //execute
            var user = userService.GetUserById("DI1");

            //assert
            Assert.NotNull(user);
            Assert.Equal(mockUser.UserId, user.UserId);

        }

        [Fact]
        public void AddUser() 
        {
            //setup

            //execute
            userService.AddUser(UserTypeEnum.RIDER,"DI1", 0,0);

            //assert
            mockDataRepo.Verify(x => x.Add(It.IsAny<User>()));

        }


        [Fact]
        public void GetDrivers()
        {
            //setup
            var driversList = new List<User>
            {
                new User { UserId = "ID2", UserType = UserTypeEnum.DRIVER, xCoord = 3, yCoord = 4 },
                new User { UserId = "ID3", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 3 },
                new User { UserId = "ID4", UserType = UserTypeEnum.DRIVER, xCoord = 4, yCoord = 4 }
            };
            mockDataRepo.Setup(x => x.GetUserByType(UserTypeEnum.DRIVER)).Returns(driversList);

            //execute
            var response = userService.GetDriversInRange(0, 0);
            
            //assert
            Assert.Equal(2, response.Count);//only 2 drivers are in range from the list above

        }

    }
}
