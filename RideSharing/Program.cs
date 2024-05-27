using Microsoft.Extensions.DependencyInjection;
using RideSharing.DataRepo;
using RideSharing.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace RideSharing
{
    internal class Program
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
            serviceProvider.GetService<RideSharingApp>().Run(cmdList);
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            // Add app
            serviceCollection.AddTransient<RideSharingApp>();

            // Add services
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IRideService, RideService>();
            serviceCollection.AddTransient<IDisplayService, DisplayService>();
            serviceCollection.AddSingleton<IAppDataRepo, AppDataRepo>();
        }
    }
}
