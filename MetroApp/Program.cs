using MetroApp.DataRepo;
using MetroApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetroApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];
            var cmdList = new List<string[]>();

            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (var commandString in File.ReadLines(@$"{filePath}"))
                {
                    string[] command = commandString.Split(' ');
                    cmdList.Add(command);
                }
            }

            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Entry to run app
            serviceProvider.GetService<MetroApp>().Run(cmdList);
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Add app
            serviceCollection.AddTransient<MetroApp>();

            // Add services
            serviceCollection.AddTransient<ICardService, CardService>();
            serviceCollection.AddTransient<ICheckInService, CheckInService>();
            serviceCollection.AddTransient<IPassengerService, PassengerService>();
            serviceCollection.AddTransient<IStationService, StationService>();
            serviceCollection.AddSingleton<IJourneyInfoRepo, JourneyInfoRepo>();
            serviceCollection.AddSingleton<ICardInfoRepo, CardInfoRepo>();
            serviceCollection.AddSingleton<IPrintService, PrintService>();
        }
    }
}
