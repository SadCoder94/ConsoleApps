using MetroApp.DataRepo;
using MetroApp.Models;
using System;

namespace MetroApp.Services
{
    public interface ICheckInService
    {
        CheckInStatus CheckIn(string cardNo, PassengerType passengerType, Station source);
    }

    public class CheckInService : ICheckInService
    {
        private readonly ICardService _cardService;
        private readonly IPassengerService _passengerService;
        private readonly IJourneyInfoRepo _journeyInfoRepo;

        public CheckInService(ICardService cardService, IPassengerService passengerService, IJourneyInfoRepo journeyInfoRepo)
        {
            _cardService = cardService;
            _passengerService = passengerService;
            _journeyInfoRepo = journeyInfoRepo;
        }

        public CheckInStatus CheckIn(string cardNo, PassengerType passengerType, Station source)
        {
            try
            {
                var passengerFee = _passengerService.GetpassengerFeeByType(passengerType);
                var currentCardAmount = _cardService.GetCardBalanceByCardNumber(cardNo);
                var discount = 0;
                var serviceCharge = 0;

                var journey = new JourneyInfo
                {
                    CardNo = cardNo,
                    StartingStation = source,
                    DestinationStation = source == Station.AIRPORT ? Station.CENTRAL : Station.AIRPORT,
                    PassengerType = passengerType
                };

                var isRoundTrip = _journeyInfoRepo.IsRoundtrip(journey);
                if (isRoundTrip)
                {
                    discount = passengerFee / 2;
                    passengerFee -= discount;//50% discount on roundtrip
                }

                //auto recharge 
                if (currentCardAmount < passengerFee)
                {
                    var amountRecharge = passengerFee - currentCardAmount;
                    _cardService.AddCardBalance(cardNo, amountRecharge);
                    serviceCharge = (int)(amountRecharge * 0.02);
                }

                //deduct charges from card
                _cardService.DeductCardBalance(cardNo, passengerFee);

                journey.Charges = passengerFee;
                journey.Discount = discount;
                journey.ServiceCharges = serviceCharge;

                _journeyInfoRepo.AddJourney(journey);

                return CheckInStatus.CHECKED_IN;
            }
            catch (CardNotFoundException)
            {
                Console.WriteLine("Card number not found");
                return CheckInStatus.ERROR_IN_CHECKIN;
            }
        }
    }

}
