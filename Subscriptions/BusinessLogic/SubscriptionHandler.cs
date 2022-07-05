using Subscriptions.DataSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriptions.BusinessLogic
{
    public class SubscriptionHandler
    {
        private SubscriptionCalculator _calculator;
        private IUserDataSource _userDataSource;
        private ISubscriptionDataSource _subscriptionDataSource;
        public SubscriptionHandler(IUserDataSource userDataSource, ISubscriptionDataSource subscriptionDataSource)
        {
            _userDataSource = userDataSource;
            _subscriptionDataSource = subscriptionDataSource;
            _calculator = new SubscriptionCalculator(_subscriptionDataSource,userDataSource);
        }

        internal bool StartSubscription(string subscriptionStartDate)
        {
            var isDateTime = DateTime.TryParse(subscriptionStartDate,out DateTime startDate);
            if (isDateTime)
            {
                _userDataSource.AddSubscriptionStartDate(startDate);
                return true;//send true if no error
            }
            else
                return false;//send false if error

        }

        internal void PrintRenewalDates()
        {
            var renewalInfo = _userDataSource.GetSubscriptionInformation();

            foreach (var info in renewalInfo)
            {
                Console.WriteLine(info);
            }
        }

        internal void AddSubscription(string serviceName, string subscriptionType)
        {
            //duplicate subs not alowed
        }

        internal void AddTopUp(string v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
