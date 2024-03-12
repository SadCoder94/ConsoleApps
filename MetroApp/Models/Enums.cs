using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp.Models
{
    public enum Station
    {
        AIRPORT,
        CENTRAL
    }

    public enum CheckInStatus
    {
        CHECKED_IN,
        LOW_CARD_BALANCE_RECHARGE
    }
    public enum PassengerType
    {
        ADULT,
        SENIOR_CITIZEN,
        KID
    }
}
