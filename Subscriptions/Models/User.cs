using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriptions.Models
{
    public class User
    {
        public List<UserSubscription> Subscriptions { get; set; }
        public int OutstandingAmount { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
    }
}
