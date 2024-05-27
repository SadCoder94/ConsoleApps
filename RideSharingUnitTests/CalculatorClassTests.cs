using Xunit;

using RideSharing.SharedMethods;

namespace RideSharingUnitTests
{
    public class CalculatorClassTests
    {
        public CalculatorClassTests() { }

        [Fact]
        public void CalculateEucledianDistance() 
        {
            var dist = CalculationMethods.GetEucledianDistance(0,0,5,5);
            Assert.Equal(7.07, dist);
        }

        [Fact]
        public void CalculateRideAmount() 
        {
            var amt = CalculationMethods.CalculateRideAmount(100, 100);
            Assert.Equal((double)1080, amt);
        }
    }
}
