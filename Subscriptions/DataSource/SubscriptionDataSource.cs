using Subscriptions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriptions.DataSource
{
    public interface ISubscriptionDataSource
    {
        Services GetServiceInformation(ServiceName serviceName);
    }

    public class SubscriptionDataSource : ISubscriptionDataSource
    {
        List<Services> subscriptions = new List<Services>();
        public SubscriptionDataSource()
        {
            subscriptions.Add(new Services
            {
                ServiceName = ServiceName.MUSIC,
                Prices = new List<ServiceInformation> {
                    new ServiceInformation { SubscriptionTimeInMonths = 1, SubscriptionCost = 0, SubscriptionType = SubscriptionType.FREE },
                    new ServiceInformation { SubscriptionTimeInMonths = 1, SubscriptionCost = 100, SubscriptionType = SubscriptionType.PERSONAL },
                    new ServiceInformation { SubscriptionTimeInMonths = 3, SubscriptionCost = 250, SubscriptionType = SubscriptionType.PREMIUM }

                    }
            });

            subscriptions.Add(new Services
            {
                ServiceName = ServiceName.VIDEO,
                Prices = new List<ServiceInformation> {
                    new ServiceInformation { SubscriptionTimeInMonths = 1, SubscriptionCost = 0, SubscriptionType = SubscriptionType.FREE },
                    new ServiceInformation { SubscriptionTimeInMonths = 1, SubscriptionCost = 200, SubscriptionType = SubscriptionType.PERSONAL },
                    new ServiceInformation { SubscriptionTimeInMonths = 3, SubscriptionCost = 500, SubscriptionType = SubscriptionType.PREMIUM }

                    }
            });

            subscriptions.Add(new Services
            {
                ServiceName = ServiceName.PODCAST,
                Prices = new List<ServiceInformation> {
                    new ServiceInformation { SubscriptionTimeInMonths = 1, SubscriptionCost = 0, SubscriptionType = SubscriptionType.FREE },
                    new ServiceInformation { SubscriptionTimeInMonths = 1, SubscriptionCost = 100, SubscriptionType = SubscriptionType.PERSONAL },
                    new ServiceInformation { SubscriptionTimeInMonths = 3, SubscriptionCost = 300, SubscriptionType = SubscriptionType.PREMIUM }

                    }
            });
        }

        public Services GetServiceInformation(ServiceName serviceName)
        {
            return subscriptions.Find(s => s.ServiceName == serviceName);
        }
    }
}
