using System;
using System.Collections.Generic;

namespace RideSharing.SharedMethods
{
    public static class CalculationMethods
    {
        public static double GetEucledianDistance(int x1, int y1, int x2, int y2)
        {
            var dist = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            return Math.Round(dist,2, MidpointRounding.AwayFromZero);
            //return dist;
        }

        public static double CalculateRideAmount(int time,double distance)
        {
            double baseFare = 50;
            double perKilometerCharge = 6.5;
            double perMinuteCharge = 2;

            double totalAmount = baseFare + (perKilometerCharge * distance) + (perMinuteCharge * time);
            double serviceCharge = 0.2 * totalAmount; // 20% of the fare

            totalAmount += serviceCharge;

            Console.WriteLine($"T-{time} D-{distance} A-{totalAmount}");
            return totalAmount;
        }
    }
}
