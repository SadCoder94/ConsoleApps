using System;
using System.Collections.Generic;
using System.Text;

namespace WaterManagement.Classes
{
    interface IApartment
    {
        public void AddGuests(int newGuests);
        public int getGuestsNumber();
        public void addBillingFractions(double corporateFraction, double borewellFraction);
        int GetTotalAllotedWater();
        double GetBoreWellFraction();
        double GetCorporationFraction();
        int GetActualConsumedWater();
    }
    public class Apartment: IApartment
    {
        public int Guests { get; set; }
        public int AllotedWater { get; set; }
        public int ActualWaterConsumption { get; set; }
        public double CorporationBillFraction { get; set; }
        public double BorewellBillFraction { get; set; }

        public Apartment()
        {

        }
        public Apartment(int apartmentType)
        {
            if (apartmentType == 2)
            {
                Guests = 3;
                AllotedWater = 900;
                ActualWaterConsumption = 900;
            }
            else if (apartmentType == 3)
            {
                Guests = 5;
                AllotedWater = 1500;
                ActualWaterConsumption = 1500;
            }
        }

        public void AddGuests(int newGuests)
        {
            Guests += newGuests;
            ActualWaterConsumption += 300*newGuests;
        }

        public int getGuestsNumber()
        {
            return Guests;
        }

        public void addBillingFractions(double corporateFraction, double borewellFraction)
        {
            CorporationBillFraction = corporateFraction;
            BorewellBillFraction = borewellFraction;
        }

        public int GetTotalAllotedWater()
        {
            return AllotedWater;
        }

        public int GetActualConsumedWater()
        {
            return ActualWaterConsumption;
        }

        public double GetBoreWellFraction()
        {
            return BorewellBillFraction;
        }

        public double GetCorporationFraction()
        {
            return CorporationBillFraction;
        }
    }
}
