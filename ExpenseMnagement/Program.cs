using ExpenseManagement.Classes;
using System;
using System.IO;

namespace ExpenseMnagement
{
    class Program
    {
        static void Main(string[] args)
        {
            String filePath = args[0];
            House house = new House();
            DueCalculator dueCalculator = new DueCalculator(house);

            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (var commandString in File.ReadLines(@$"{filePath}"))
                {
                    string[] command = commandString.Split(' ');
                    switch (command[0])
                    {
                        case "MOVE_IN":
                            house.MoveInMember(command[1]);
                            break;
                        case "SPEND":
                            dueCalculator.Spend(commandString);
                            break;
                        case "DUES":
                            dueCalculator.Dues(command[1]);
                            break;
                        case "CLEAR_DUE":
                            dueCalculator.ClearDues(commandString);
                            break;
                        case "MOVE_OUT":
                            house.MoveOutMember(command[1]);
                            break;
                    }
                }
            }

        }
    }
}
