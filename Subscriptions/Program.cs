using Subscriptions.BusinessLogic;
using Subscriptions.DataSource;
using System;
using System.IO;

namespace Subscriptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String filePath = args[0];
            bool noErrorDetected = true;

            var userDataSource = new UserDataSource();
            var subscriptionDataSource = new SubscriptionDataSource();
            var subscriptionHandler = new SubscriptionHandler(userDataSource,subscriptionDataSource);

            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (var commandString in File.ReadLines(@$"{filePath}"))
                {
                    if (!noErrorDetected) break;

                    string[] command = commandString.Split(' ');
                    switch (command[0])
                    {
                        case "START_SUBSCRIPTION":
                            noErrorDetected = subscriptionHandler.StartSubscription(command[1]);
                            break;
                        case "ADD_SUBSCRIPTION":
                            subscriptionHandler.AddSubscription(command[1], command[2]);
                            break;
                        case "ADD_TOPUP":
                            subscriptionHandler.AddTopUp(command[1], Int32.Parse(command[2]));
                            break;
                        case "PRINT_RENEWAL_DETAILS":
                            subscriptionHandler.PrintRenewalDates();
                            break;
                    }
                }
            }
        }
    }
}
