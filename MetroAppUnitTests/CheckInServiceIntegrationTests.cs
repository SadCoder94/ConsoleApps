using MetroApp.DataRepo;
using MetroApp.Models;
using MetroApp.Services;
using Moq;
using Xunit;

namespace MetroAppUnitTests
{
    public class CheckInServiceIntegrationTests
    {
        private Mock<ICardService> _cardService;
        private Mock<IPassengerService> _passengerService;
        private Mock<IJourneyInfoRepo> _journeyRepo;
        private CheckInService _checkInService;

        public CheckInServiceIntegrationTests()
        {
            _cardService = new Mock<ICardService>();
            _passengerService = new Mock<IPassengerService>();
            _journeyRepo = new Mock<IJourneyInfoRepo>();
            _checkInService = new CheckInService(_cardService.Object, _passengerService.Object, _journeyRepo.Object);
        }

        [Fact]
        public void TestForNormalTrip()
        {
            //setup
            _cardService.Setup(d => d.GetCardBalanceByCardNumber("MC1")).Returns(400);
            _passengerService.Setup(d => d.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN)).Returns(100);
            _journeyRepo.Setup(d => d.IsRoundtrip(It.IsAny<JourneyInfo>())).Returns(false);

            //run
            var result = _checkInService.CheckIn("MC1", PassengerType.SENIOR_CITIZEN, Station.CENTRAL);

            //verify
            Assert.Equal(CheckInStatus.CHECKED_IN,result);
            _cardService.Verify(m => m.GetCardBalanceByCardNumber("MC1"), Times.Once);
            _passengerService.Verify(m => m.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN), Times.Once);
            _cardService.Verify(m => m.DeductCardBalance("MC1", 100), Times.Once);

            _journeyRepo.Verify(m => m.AddJourney(It.IsAny<JourneyInfo>()), Times.Once);
            _journeyRepo.Verify(m => m.AddJourney(It.Is<JourneyInfo>(j =>
                j.CardNo == "MC1" &&
                j.PassengerType == PassengerType.SENIOR_CITIZEN &&
                j.StartingStation == Station.CENTRAL &&
                j.DestinationStation == Station.AIRPORT &&
                j.Discount == 0 &&
                j.Charges == 100 &&
                j.ServiceCharges == 0
                )), Times.Once);
        }

        [Fact]
        public void TestForRoundTrip()
        {
            //setup
            _cardService.Setup(d => d.GetCardBalanceByCardNumber("MC1")).Returns(400);
            _passengerService.Setup(d => d.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN)).Returns(100);
            _journeyRepo.Setup(d => d.IsRoundtrip(It.IsAny<JourneyInfo>())).Returns(true);

            //run
            var result = _checkInService.CheckIn("MC1", PassengerType.SENIOR_CITIZEN, Station.CENTRAL);

            //verify
            Assert.Equal(CheckInStatus.CHECKED_IN, result);
            _cardService.Verify(m => m.GetCardBalanceByCardNumber("MC1"), Times.Once);
            _passengerService.Verify(m => m.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN), Times.Once);
            _cardService.Verify(m => m.DeductCardBalance("MC1", 50), Times.Once);

            _journeyRepo.Verify(m => m.AddJourney(It.IsAny<JourneyInfo>()), Times.Once);
            _journeyRepo.Verify(m => m.AddJourney(It.Is<JourneyInfo>(j =>
                j.CardNo == "MC1" &&
                j.PassengerType == PassengerType.SENIOR_CITIZEN &&
                j.StartingStation == Station.CENTRAL &&
                j.DestinationStation == Station.AIRPORT &&
                j.Discount == 50 &&
                j.Charges == 50 &&
                j.ServiceCharges == 0
                )), Times.Once);
        }

        [Fact]
        public void TestForNotEnoughBalance()
        {
            //setup
            _cardService.Setup(d => d.GetCardBalanceByCardNumber("MC1")).Returns(50);
            _passengerService.Setup(d => d.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN)).Returns(100);
            _journeyRepo.Setup(d => d.IsRoundtrip(It.IsAny<JourneyInfo>())).Returns(false);

            //run
            var result = _checkInService.CheckIn("MC1", PassengerType.SENIOR_CITIZEN, Station.CENTRAL);

            //verify
            Assert.Equal(CheckInStatus.CHECKED_IN, result);
            _cardService.Verify(m => m.GetCardBalanceByCardNumber("MC1"), Times.Once);
            _passengerService.Verify(m => m.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN), Times.Once);
            _cardService.Verify(m => m.AddCardBalance("MC1", 50), Times.Once);
            _cardService.Verify(m => m.DeductCardBalance("MC1", 100), Times.Once);

            _journeyRepo.Verify(m => m.AddJourney(It.IsAny<JourneyInfo>()), Times.Once);
            _journeyRepo.Verify(m => m.AddJourney(It.Is<JourneyInfo>(j =>
                j.CardNo == "MC1"
                && j.PassengerType == PassengerType.SENIOR_CITIZEN
                && j.StartingStation == Station.CENTRAL
                && j.DestinationStation == Station.AIRPORT
                && j.Discount == 0
                && j.Charges == 100
                && j.ServiceCharges == 1
                )), Times.Once);
        }

        [Fact]
        public void TestForNotEnoughBalanceOnRoundTrip()
        {
            //setup
            _cardService.Setup(d => d.GetCardBalanceByCardNumber(It.IsAny<string>())).Returns(50);
            _passengerService.Setup(d => d.GetpassengerFeeByType(It.IsAny<PassengerType>())).Returns(100);
            _journeyRepo.Setup(d => d.IsRoundtrip(It.IsAny<JourneyInfo>())).Returns(true);

            //run
            var result = _checkInService.CheckIn("MC1", PassengerType.SENIOR_CITIZEN, Station.CENTRAL);

            //verify
            Assert.Equal(CheckInStatus.CHECKED_IN, result);
            _cardService.Verify(m => m.GetCardBalanceByCardNumber("MC1"), Times.Once);
            _passengerService.Verify(m => m.GetpassengerFeeByType(PassengerType.SENIOR_CITIZEN), Times.Once);
            //_cardService.Verify(m => m.AddCardBalance("MC1", 50), Times.Once);
            _cardService.Verify(m => m.DeductCardBalance("MC1", 50), Times.Once);

            _journeyRepo.Verify(m => m.AddJourney(It.IsAny<JourneyInfo>()), Times.Once);
            _journeyRepo.Verify(m => m.AddJourney(It.Is<JourneyInfo>(j =>
                j.CardNo == "MC1"
                && j.PassengerType == PassengerType.SENIOR_CITIZEN
                && j.StartingStation == Station.CENTRAL
                && j.DestinationStation == Station.AIRPORT
                && j.Discount == 50
                && j.Charges == 50
                && j.ServiceCharges == 0
                )), Times.Once);
        }


    }
}
