using Subscriptions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriptions.DataSource
{
    public interface IUserDataSource
    {
        void AddSubscription(UserSubscription subscription);
        void AddSubscriptionStartDate(DateTime subscriptionStart);
        List<string> GetSubscriptionInformation();
        void UpdateOutstandingAmount(int amount);
        DateTime GetSubscriptionStartDate();
    }

    public class UserDataSource : IUserDataSource
    {
        private User user;
        public UserDataSource()
        {
            user = new User();
        }
        
        public List<string> GetSubscriptionInformation()
        {
            var subscriptionInformation = new List<string>();

            foreach (var subscription in user.Subscriptions)
            {
                subscriptionInformation.Add("RENEWAL_REMINDER " + subscription.ServiceName.ToString() + " " + subscription.ReminderDate.ToString("dd-MM-yyyy"));
            }

            subscriptionInformation.Add("RENEWAL_AMOUNT " + subscriptionInformation);

            return subscriptionInformation;
        }

        public void AddSubscription(UserSubscription subscription)
        {
            user.Subscriptions.Add(subscription);
        }

        public void AddSubscriptionStartDate(DateTime subscriptionStart)
        {
            user.SubscriptionStartDate = subscriptionStart;
        }

        public void UpdateOutstandingAmount(int amount)
        {
            user.OutstandingAmount += amount;
        }

        public DateTime GetSubscriptionStartDate()
        {
            return user.SubscriptionStartDate;
        }

    }
}
