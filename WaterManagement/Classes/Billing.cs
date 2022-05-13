using System;
using System.Collections.Generic;
using System.Text;

namespace WaterManagement.Classes
{
    interface IBilling
    {
        public Apartment AddNewApartMentBilling(int apartmentType, string ratio);
        public void GenerateBill(Apartment apartment);
    }
    public class Billing: IBilling
    {
        public Apartment AddNewApartMentBilling(int apartmentType, string ratio)
        {
            Apartment apartment = new Apartment(apartmentType);
            string[] ratios = ratio.Split(':');
            var corporationBillFraction = double.Parse(ratios[0]) / (double.Parse(ratios[0]) + double.Parse(ratios[1]));
            var borewellBillFraction = double.Parse(ratios[1]) / (double.Parse(ratios[0]) + double.Parse(ratios[1]));
            apartment.addBillingFractions(corporationBillFraction, borewellBillFraction);

            return apartment;
        }

        public void GenerateBill(Apartment apartment)
        {
            double corporationRate = 1;
            double borewellRate = 1.5;
            double allotedWater = apartment.GetTotalAllotedWater();
            double corporationBill = apartment.GetCorporationFraction() * allotedWater * corporationRate;
            double borewellBill = apartment.GetBoreWellFraction() * allotedWater * borewellRate;
            double bill = corporationBill + borewellBill;

            var totalConsumedWater = apartment.GetActualConsumedWater();
            var excessConsumedWater = totalConsumedWater - allotedWater;
            double tankerBill = 0.0;

            if (excessConsumedWater > 0 && excessConsumedWater <= 500)//0 - 500
            {
                tankerBill = excessConsumedWater * 2;
            }

            if (excessConsumedWater > 500 && excessConsumedWater <= 1500)//501 - 1500
            {
                tankerBill = 500 * 2 + (excessConsumedWater - 500) * 3;
            }

            if (excessConsumedWater > 1500 && excessConsumedWater <= 3000 )//1501 - 3000
            {
                tankerBill = 500 * 2 + 1000 * 3 + (excessConsumedWater - 1500) * 5;
            }

            if (excessConsumedWater > 3000)
            {
               tankerBill = 500 * 2 + 1000 * 3 + 1500 * 5 + (excessConsumedWater - 3000) * 8;
            }

            bill += tankerBill;

            Console.WriteLine(totalConsumedWater + " "+ Math.Ceiling(bill).ToString());
        }

    }
}
