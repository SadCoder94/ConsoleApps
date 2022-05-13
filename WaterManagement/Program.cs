using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using WaterManagement.Classes;

namespace WaterManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            String filePath = args[0];
            Billing billing = new Billing();
            Apartment newApartment = new Apartment(); 

            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (var commandString in File.ReadLines(@$"{filePath}"))
                {
                    string[] command = commandString.Split(' ');
                    switch (command[0])
                    {
                        case "ALLOT_WATER":
                            newApartment = billing.AddNewApartMentBilling(int.Parse(command[1]), command[2]);
                            break;
                        case "ADD_GUESTS":
                            newApartment.AddGuests(int.Parse(command[1]));
                            break;
                        case "BILL":
                            billing.GenerateBill(newApartment);
                            break;
                    }
                }
            }
        }
    }
}
