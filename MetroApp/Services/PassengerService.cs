using MetroApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MetroApp.Services
{
    public interface IPassengerService
    {
        int GetpassengerFeeByType(PassengerType passengerType);
    }

    public class PassengerService : IPassengerService
    {
        private List<Passenger> _passengers;

        public PassengerService()
        {
            _passengers = new List<Passenger>() {
            new Passenger { PassengerType = PassengerType.ADULT, PassengerFee = 200 },
            new Passenger { PassengerType = PassengerType.SENIOR_CITIZEN, PassengerFee = 100 },
            new Passenger { PassengerType = PassengerType.KID, PassengerFee = 50 }};

        }

        public int GetpassengerFeeByType(PassengerType passengerType)
        {
            return _passengers.FirstOrDefault(x => x.PassengerType == passengerType).PassengerFee;
        }
    }
}
