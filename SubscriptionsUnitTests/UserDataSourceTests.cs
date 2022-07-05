using Subscriptions.DataSource;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SubscriptionsUnitTests
{
    public class UserDataSourceTests
    {
        [Fact]
        public void SetAndGetUserSubscriptionDate()
        {
            var source = new UserDataSource();

            source.AddSubscriptionStartDate(new DateTime(2021,10,20));
            var getSubscriptionDate = source.GetSubscriptionStartDate();
            Assert.Equal(new DateTime(2021,10,20), getSubscriptionDate);
        }
    }
}
