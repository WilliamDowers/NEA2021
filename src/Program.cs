using System;
using System.Collections.Generic;
using System.IO;

namespace NEA2020
{
    static class Program
    {
        static List<Airport> Airports;
        static List<Plane> Planes;
        static ConsoleColor consoleColor;


        static void Main()
        {
            consoleColor = ConsoleColor.Red; // The color that indicated the current selection on the console
            Airports = new List<Airport>();
            Planes = new List<Plane>();
            GenerateAirports();
            GeneratePlanes();
            Menu();
        }

        static void Menu() // main menu
        {
            int selection = 0;
            string[] menu = new string[] {  " 1.  Enter airport details" ,
                                            " 2.  Enter flight details" ,
                                            " 3.  Enter price plan and calculate profit" ,
                                            " 4.  Clear data" ,
                                            " 5.  Quit"  };
            for (bool shouldExit = false; shouldExit == false;)
            {
                Console.Clear();

                for (int i = 0; i < menu.Length; i++)
                {

                    if (selection == i) { Console.ForegroundColor = consoleColor; }
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }

                var keyPress = Console.ReadKey();
                switch (keyPress.Key)
                {
                    case ConsoleKey.UpArrow: selection--; break;
                    case ConsoleKey.DownArrow: selection++; break;
                    case ConsoleKey.Enter: shouldExit = true; break;
                    default: break;
                }

                if (selection < 0) { selection = 4; }
                else if (selection > 4) { selection = 0; }
            }

            switch (selection)
            {
                case 0: EnterAirportDetails(); break;
                case 1: EnterFlightDetails(); break;
                case 2: EnterPricePlanAndCalculateProfit(); break;
                case 3: ClearData(); break;
                case 4: Console.WriteLine("Quitting..."); Environment.Exit(0); break;
                default: ReturnToMenu("Input must be between '1' and '5'"); break;
            }
        }

        static void GenerateAirports() // Populate Airports with data from Airports.csv
        {
            string[] seperatedAirportsText = File.ReadAllText(@"../../../Airports.csv").Split('\n');
            foreach (var seperatedAirport in seperatedAirportsText)
            {
                string[] tempAirport = seperatedAirport.Split(',');
                Airports.Add(new Airport(tempAirport[0], tempAirport[1], Int32.Parse(tempAirport[2]), Int32.Parse(tempAirport[3])));
            }
        }

        static void GeneratePlanes() // Populate Planes with data from Planes.csv 
        {
            string[] seperatedPlanesText = File.ReadAllText(@"../../../Planes.csv").Split('\n');
            foreach (var seperatedPlane in seperatedPlanesText)
            {
                string[] tempPlane = seperatedPlane.Split(',');
                Planes.Add(new Plane(tempPlane[0], Int32.Parse(tempPlane[1]), Int32.Parse(tempPlane[2]), Int32.Parse(tempPlane[3]), Int32.Parse(tempPlane[4])));
            }
        }

        static void EnterAirportDetails() // Enter both UK and overseas airport
        {
            string ukAirport = null;
            Airport overseasAirport = new Airport();
            // Enter UK airport
            int selection = 0;
            string[] options = new string[] { "BOH", "LPL" };
            for (bool shouldExit = false; shouldExit == false;)
            {
                Console.Clear();
                Console.WriteLine("Enter UK Airport: \n");
                for (int i = 0; i < 2; i++)
                {
                    if (i == selection) { Console.ForegroundColor = consoleColor; }
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }
                var keyPress = Console.ReadKey();
                switch (keyPress.Key)
                {
                    case ConsoleKey.UpArrow: selection--; break;
                    case ConsoleKey.DownArrow: selection++; break;
                    case ConsoleKey.Enter: shouldExit = true; break;
                    default: break;
                }

                if (selection > 1) { selection = 0; }
                else if (selection < 0) { selection = 1; }
            }

            switch (selection)
            {
                case 0: ukAirport = "Bournemouth International"; break;
                case 1: ukAirport = "Liverpool John Lennon"; break;
            }

            // Enter overseas airport
            selection = 0;
            for (bool shouldExit = false; shouldExit == false;)
            {
                Console.Clear();
                Console.WriteLine("Enter overseas airport: \n");
                for (int i = 0; i < Airports.Count; i++)
                {
                    if (i == selection) { Console.ForegroundColor = consoleColor; }
                    Console.WriteLine(Airports[i].code);
                    Console.ResetColor();
                }
                var keyPress = Console.ReadKey();
                switch (keyPress.Key)
                {
                    case ConsoleKey.UpArrow: selection--; break;
                    case ConsoleKey.DownArrow: selection++; break;
                    case ConsoleKey.Enter: overseasAirport = Airports[selection]; shouldExit = true; break;
                }

                if (selection < 0) { selection = Airports.Count - 1; }
                else if (selection > Airports.Count - 1) { selection = 0; }

            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" __________________________________________________" +
                                        "\n                                          " +
                                        "\n  UK Airport:          " + ukAirport +
                                        "\n  Overseas Airport:    " + overseasAirport.name +
                                        "\n __________________________________________________");
                Console.WriteLine("\n Press Enter to continue...");
                var keyPress = Console.ReadKey();
                if (keyPress.Key == ConsoleKey.Enter) { break; }
            }

            FlightPlan.ukAirport = ukAirport;
            FlightPlan.overseasAirport = overseasAirport;
            Menu();
        }

        static void EnterFlightDetails() // Enter flight details
        {
            Plane tempPlane = new Plane();
            int firstClassNum;
            {
                int selection = 0;
                for (bool shouldExit = false; shouldExit == false;)
                {
                    Console.Clear();
                    Console.WriteLine("Enter Aircraft Type: \n");
                    for (int i = 0; i < Planes.Count; i++)
                    {
                        if (selection == i) { Console.ForegroundColor = consoleColor; }
                        Console.WriteLine(Planes[i].type);
                        Console.ResetColor();
                    }
                    var keyPress = Console.ReadKey();
                    switch (keyPress.Key)
                    {
                        case ConsoleKey.UpArrow: selection--; break;
                        case ConsoleKey.DownArrow: selection++; break;
                        case ConsoleKey.Enter: tempPlane = Planes[selection]; shouldExit = true; break;
                    }

                    if (selection < 0) { selection = Planes.Count - 1; }
                    else if (selection > Planes.Count - 1) { selection = 0; }
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(" _______________________________________________________________" +
                                            "\n                                                     " +
                                            "\n  Type:                                        " + tempPlane.type +
                                            "\n  Running cost (per seat per 100km):           " + "£" + tempPlane.runningCostPerSeatPer100Kilometres +
                                            "\n  Maximum flight range:                        " + tempPlane.maxFlightRange + " kilometres" +
                                            "\n  Capacity (if all seats are standard-class):  " + tempPlane.capIfAllStandard +
                                            "\n  Minimum first class seats (if any):          " + tempPlane.minFirstClass +
                                            "\n _______________________________________________________________");
                    Console.WriteLine("\n Press Enter to continue...");
                    var keyPress = Console.ReadKey();
                    if (keyPress.Key == ConsoleKey.Enter) { break; }
                }
            }

            while (true) // input number of first-class seats and error handling
            {
                Console.Clear();
                if (!int.TryParse(UserInput("Input number of first-class seats: "), out firstClassNum)) { ThrowError(); continue; }
                if ((firstClassNum > 0) && (firstClassNum < tempPlane.minFirstClass)) { ThrowError("First-class seating must exceed minimum, if there are any."); continue; }
                else if (firstClassNum > (tempPlane.capIfAllStandard / 2)) { ThrowError("Exceeds maximum seating"); continue; }
                break;
            }
            FlightPlan.plane = tempPlane;
            FlightPlan.firstClassNum = firstClassNum;
            FlightPlan.standardClassNum = FlightPlan.plane.capIfAllStandard - (FlightPlan.firstClassNum * 2);
            Menu();
        }

        static void EnterPricePlanAndCalculateProfit() // Enter price plan and calculate profit
        {
            // error handling
            Console.Clear();
            int tempDistFromUk = 0;

            string errors = null;
            bool ukAirportNull = false;
            bool overseasAirportNull = false;
            bool planeNull = false;
            bool notWithinFlightRange = false;
            bool missingFields = false;

            if (FlightPlan.ukAirport == null) { ukAirportNull = true; missingFields = true; }
            if (FlightPlan.overseasAirport == null) { overseasAirportNull = true; missingFields = true; }
            if (FlightPlan.plane == null) { planeNull = true; missingFields = true; }
            if (!ukAirportNull && !planeNull)
            {
                switch (FlightPlan.ukAirport)
                {
                    case "Bournemouth International":
                        if (FlightPlan.plane.maxFlightRange < FlightPlan.overseasAirport.distFromBI) { notWithinFlightRange = true; missingFields = true; break; }
                        tempDistFromUk = FlightPlan.overseasAirport.distFromBI;
                        break;
                    case "Liverpool John Lennon":
                        if (FlightPlan.plane.maxFlightRange < FlightPlan.overseasAirport.distFromJL) { notWithinFlightRange = true; missingFields = true; break; }
                        tempDistFromUk = FlightPlan.overseasAirport.distFromJL;
                        break;
                }
            }

            if (ukAirportNull) { errors += "\nUK Airport is missing"; }
            if (overseasAirportNull) { errors += "\nOverseas Airport is missing"; }
            if (planeNull) { errors += "\nAircraft type is missing"; }
            if (notWithinFlightRange) { errors += "\nDistance exceeds maximum flight range"; }
            if (missingFields) { ReturnToMenu(errors); }


            // Input standard-class and first-class seat prices, and attempt to convert to int, else return to main menu
            if (!Int32.TryParse(UserInput("\nInput Standard-Class Seat Price:  £"), out int standardClassPrice)) { ReturnToMenu(); }
            Console.Clear();
            if (!Int32.TryParse(UserInput("\nInput First-Class Seat Price:  £"), out int firstClassPrice)) { ReturnToMenu(); }



            // Fill FlightPlan singleton
            FlightPlan.standardClassPrice = standardClassPrice;
            FlightPlan.firstClassPrice = firstClassPrice;
            FlightPlan.distance = tempDistFromUk;
            FlightPlan.flightCostPerSeat = FlightPlan.plane.runningCostPerSeatPer100Kilometres * FlightPlan.distance / 100;
            FlightPlan.flightCost = FlightPlan.flightCostPerSeat * (FlightPlan.firstClassNum + FlightPlan.standardClassNum);
            FlightPlan.flightIncome = FlightPlan.firstClassNum * FlightPlan.firstClassPrice + FlightPlan.standardClassNum * FlightPlan.standardClassPrice;
            FlightPlan.profit = FlightPlan.flightIncome - FlightPlan.flightCost;

            while (true) // Output FlightPlan, then return to menu
            {
                Console.Clear();
                Console.WriteLine(" __________________________________________________________________________" +
                                        "\n                                                                           " +
                                        "\n  UK Airport:                                  " + FlightPlan.ukAirport +
                                        "\n  Overseas Airport:                            " + FlightPlan.overseasAirport.name +
                                        "\n  Distance:                                    " + FlightPlan.distance + " kilometres" +
                                        "\n  Type of aircraft:                            " + FlightPlan.plane.type +
                                        "\n  Maximum flight range:                        " + FlightPlan.plane.maxFlightRange + " kilometres" +
                                        "\n  Running cost (per seat per 100km):           " + "£" + FlightPlan.plane.runningCostPerSeatPer100Kilometres +
                                        "\n  Capacity (if all seats are standard-class):  " + FlightPlan.plane.capIfAllStandard +
                                        "\n  Number of first-class seats:                 " + FlightPlan.firstClassNum +
                                        "\n  Number of standard-class seats:              " + FlightPlan.standardClassNum +
                                        "\n  Price of a standard-class seat:              " + "£" + FlightPlan.standardClassPrice +
                                        "\n  Price of a first-class seat:                 " + "£" + FlightPlan.firstClassPrice +
                                        "\n  Flight cost per seat:                        " + "£" + FlightPlan.flightCostPerSeat +
                                        "\n  Flight cost:                                 " + "£" + FlightPlan.flightCost +
                                        "\n  Flight income:                               " + "£" + FlightPlan.flightIncome +
                                        "\n  Flight profit:                               " + "£" + FlightPlan.profit +
                                        "\n __________________________________________________________________________");
                Console.WriteLine("\nPress Enter to continue...");
                var keyPress = Console.ReadKey();
                if (keyPress.Key == ConsoleKey.Enter) { Menu(); }
            }


        }


        static void ClearData() // Clears all data in the FlightPlan singleton
        {
            FlightPlan.ClearData();
            ReturnToMenu("Data cleared");
        }

        static void ReturnToMenu(string error = "Invalid Input") // Outputs error message to the console, pauses application until user presses enter and returns to main menu
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(error);
                Console.WriteLine("\nPress Enter to return to menu...");
                var keyPress = Console.ReadKey();
                if (keyPress.Key == ConsoleKey.Enter) { Menu(); }

            }
        }

        static void ThrowError(string error = "Invalid Input") // Outputs error message to the console which pauses the application
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(error);
                Console.WriteLine("\nPress Enter to continue...");
                var keyPress = Console.ReadKey();
                if (keyPress.Key == ConsoleKey.Enter) { return; }

            }
        }

        static string UserInput(string message) // Outputs message, waits for user input and returns input
        {
            Console.Write(message);
            string response = Console.ReadLine();
            Console.WriteLine();
            return response;
        }
    }
}
