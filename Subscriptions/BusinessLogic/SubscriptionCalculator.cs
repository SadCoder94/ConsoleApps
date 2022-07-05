using Subscriptions.DataSource;
using Subscriptions.Models;

namespace Subscriptions.BusinessLogic
{
    public class SubscriptionCalculator
    {
        private readonly ISubscriptionDataSource _subscriptionData;
        private readonly IUserDataSource _userDataSource;
        public SubscriptionCalculator(ISubscriptionDataSource subscriptionData, IUserDataSource userDataSource)
        {
            _subscriptionData = subscriptionData;
            _userDataSource = userDataSource;
        }

        public void AddSubscription(ServiceName serviceName, SubscriptionType subscriptionType)
        {
            var startDate = _userDataSource.GetSubscriptionStartDate();
            var serviceInfo = _subscriptionData.GetServiceInformation(serviceName);
            var subscriptionInfo = serviceInfo.Prices.Find(x => x.SubscriptionType == subscriptionType);

            var reminderDate = startDate.AddMonths(subscriptionInfo.SubscriptionTimeInMonths);
            reminderDate.AddDays(-10);

            _userDataSource.AddSubscription(new UserSubscription { ServiceName = serviceName, ReminderDate = reminderDate });
            _userDataSource.UpdateOutstandingAmount(subscriptionInfo.SubscriptionCost);
        }

        public void AddTopup(Topup topup, int months)
        {
            if (topup == Topup.FOUR_DEVICE)
                _userDataSource.UpdateOutstandingAmount(50 * months);
            if (topup == Topup.TEN_DEVICE)
                _userDataSource.UpdateOutstandingAmount(100 * months);
        }

    }
}
