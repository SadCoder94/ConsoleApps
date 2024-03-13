using MetroApp.DataRepo;
using MetroApp.Models;
using MetroApp.Services;
using Moq;
using Xunit;

namespace MetroAppUnitTests
{
    public class StationServiceUnitTests
    {
        private Mock<IJourneyInfoRepo> _mockJourneyRepo;
        private StationService _stationService;

        public StationServiceUnitTests()
        {
            _mockJourneyRepo = new Mock<IJourneyInfoRepo>();
            _stationService = new StationService(_mockJourneyRepo.Object);
        }

        [Fact]
        public void TestStationSummary()
        {
            _mockJourneyRepo.Setup(d => d.GetStationSummary(Station.CENTRAL)).Returns(new StationSummary
            {
                passengersOutbound = new System.Collections.Generic.Dictionary<PassengerType, int>
                {
                    { PassengerType.KID, 3},
                    { PassengerType.ADULT, 5},
                    { PassengerType.SENIOR_CITIZEN, 2}
                },
                station = Station.CENTRAL,
                StationCollection = 2320,
                TotalDiscount = 500
            });

            var result = _stationService.GetStationSummary(Station.CENTRAL);

            Assert.NotNull(result);
            Assert.Equal(500, result.TotalDiscount);
            Assert.Equal(2320, result.StationCollection);
        }

    }
}
