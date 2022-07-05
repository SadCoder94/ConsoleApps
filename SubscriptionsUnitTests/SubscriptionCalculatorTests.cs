using Subscriptions.BusinessLogic;
using Subscriptions.DataSource;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SubscriptionsUnitTests
{
    public class SubscriptionCalculatorTests
    {
        private UserDataSource _userDataSource; 
        private SubscriptionDataSource _subscriptionDataSource;
        private SubscriptionCalculator _subscriptionCalculator;

        [Fact]
        public void AddingSubscriptionTests()
        {
            _subscriptionCalculator.AddSubscription(Subscriptions.Models.ServiceName.MUSIC,Subscriptions.Models.SubscriptionType.PERSONAL);

            var listOfSubscriptions = _userDataSource.GetSubscriptionInformation();
            
            Assert.Single(listOfSubscriptions);
            Assert.Single(listOfSubscriptions.Find(x=>x.Contains("MUSIC")));

        }

        private void Setup()
        {
            _userDataSource = new UserDataSource();
            _subscriptionDataSource = new SubscriptionDataSource();
            _subscriptionCalculator = new SubscriptionCalculator(_subscriptionDataSource, _userDataSource);

            _userDataSource.AddSubscriptionStartDate(new DateTime(2021,4,23));
        }
    }
}
