using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriptions.Models
{
    public class Services
    {
        public ServiceName ServiceName { get; set; }
        public List<ServiceInformation> Prices { get; set; }
    }

    public enum ServiceName
    {
        MUSIC,
        VIDEO,
        PODCAST
    }

    public class ServiceInformation
    {
        public SubscriptionType SubscriptionType { get; set; }
        public int SubscriptionTimeInMonths { get; set; }
        public int SubscriptionCost { get; set; }
    }

    public enum SubscriptionType
    {
        FREE,
        PERSONAL,
        PREMIUM
    }

    public enum Topup
    {
        FOUR_DEVICE,
        TEN_DEVICE
    }
}
