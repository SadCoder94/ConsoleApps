using System;
using WaterManagement.Classes;
using Xunit;

namespace WaterManagementUnitTest
{
    public class ApartmentTests
    {
        [Fact]
        public void NewApartmentType()
        {
            Apartment apartment = new Apartment(2);
            Assert.Equal(900, apartment.AllotedWater);
        }

        [Fact]
        public void AddingGuests()
        {
            Apartment apartment = new Apartment(2);
            apartment.AddGuests(2);
            Assert.Equal(900 + 300 * 2, apartment.ActualWaterConsumption);
        }
    }
}
