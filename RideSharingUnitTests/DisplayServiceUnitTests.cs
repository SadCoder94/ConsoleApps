using Moq;
using RideSharing.Models;
using RideSharing.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace RideSharingUnitTests
{
    public class DisplayServiceUnitTests
    {
        private Mock<IRideService> _mockRideService;
        private IDisplayService _displayService;
        public DisplayServiceUnitTests()
        {
            _mockRideService = new Mock<IRideService>();
            _displayService = new DisplayService(_mockRideService.Object);
        }

        [Fact]
        public void DisplayBillForCompletedRide()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var mockData = new Ride { RideId = "R1", Amount = 123.23, DriverId = "D1", RiderId = "Ri2", StartX = 0, StartY = 0, StopX = 2, StopY = 5 };
            _mockRideService.Setup(x => x.GetRideInfoByRideId("R1")).Returns(mockData);
            
            //execution
            _displayService.DisplayBill("R1");
            
            //validations
            _mockRideService.Verify(x => x.GetRideInfoByRideId("R1"), Times.Once);
            Assert.Equal($"BILL {mockData.RideId} {mockData.DriverId} {mockData.Amount}", stringWriter.ToString().Replace("\r\n",""));
        }

        [Fact]
        public void DisplayBillForIncompleteRide()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var mockData = new Ride { RideId = "R1", DriverId = "D1", RiderId = "Ri2", StartX = 0, StartY = 0 };
            _mockRideService.Setup(x => x.GetRideInfoByRideId("R1")).Returns(mockData);

            //execution
            _displayService.DisplayBill("R1");

            //validations
            _mockRideService.Verify(x => x.GetRideInfoByRideId("R1"), Times.Once);
            Assert.Equal($"RIDE_NOT_COMPLETED", stringWriter.ToString().Replace("\r\n", ""));
        }

        [Fact]
        public void DisplayBillForNonExistentRide()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Ride mockData = null;
            _mockRideService.Setup(x => x.GetRideInfoByRideId("R1")).Returns(mockData);

            //execution
            _displayService.DisplayBill("R1");

            //validations
            _mockRideService.Verify(x => x.GetRideInfoByRideId("R1"), Times.Once);
            Assert.Equal($"INVALID_RIDE", stringWriter.ToString().Replace("\r\n", ""));
        }

        [Fact]
        public void DisplayDriverNamesIfNonEmptyList()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var list = new List<string> { "d1", "d2", "d3"};

            //execute
            _displayService.DisplayDrivers(list);

            //validations
            Assert.Equal($"DRIVERS_MATCHED d1 d2 d3", stringWriter.ToString().Replace("\r\n", ""));
        }

        [Fact]
        public void DisplayDriverNamesIfEmptyList()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var list = new List<string> { };

            //execute
            _displayService.DisplayDrivers(list);

            //validations
            Assert.Equal($"NO_DRIVERS_AVAILABLE", stringWriter.ToString().Replace("\r\n", ""));
        }

        [Fact]
        public void DisplayRideStatusForStartedRide()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //execute
            _displayService.DisplayRideStatus(RideStatus.STARTED, "abc");

            //validations
            Assert.Equal($"RIDE_STARTED abc", stringWriter.ToString().Replace("\r\n", ""));
        }

        [Fact]
        public void DisplayRideStatusIfInvalidRide()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //execute
            _displayService.DisplayRideStatus(RideStatus.INVALID, "abc");

            //validations
            Assert.Equal($"INVALID_RIDE", stringWriter.ToString().Replace("\r\n", ""));
        }

        [Fact]
        public void DisplayRideStatusForStoppedRide()
        {
            //setup
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //execute
            _displayService.DisplayRideStatus(RideStatus.STOPPED, "abc");

            //validations
            Assert.Equal($"RIDE_STOPPED abc", stringWriter.ToString().Replace("\r\n", ""));
        }
    }
}
