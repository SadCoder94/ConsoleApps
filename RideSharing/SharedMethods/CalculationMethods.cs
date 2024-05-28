using System;
using System.Collections.Generic;

namespace RideSharing.SharedMethods
{
    public static class CalculationMethods
    {
        public static double GetEucledianDistance(int x1, int y1, int x2, int y2)
        {
            var dist = Math.Abs(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
            return Math.Round(dist,2);
        }

        public static double CalculateRideAmount(int time,double distance)
        {
            var amount = 50.0 //base fare
                         + 6.5 * distance //6.5 per kilometer
                         + 2 * time; //2 per minute in ride

            amount += 0.2 * amount;// 20% service charge

            return Math.Round(amount, 2);
        }
    }
}
