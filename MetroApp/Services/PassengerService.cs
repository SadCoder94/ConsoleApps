using MetroApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.Services
{
    public interface IPassengerService
    {
        Passenger GetpassengerInfoByType(PassengerType passengerType);
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

        public Passenger GetpassengerInfoByType(PassengerType passengerType)
        {
            return _passengers.FirstOrDefault(x => x.PassengerType == passengerType);
        }
    }
}
