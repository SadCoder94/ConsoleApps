using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriptions.Models
{
    public class UserSubscription
    {
        public ServiceName ServiceName { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
        