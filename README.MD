# NEA 2021 

## Introduction 
This project is a basic flight information calculator, which is designed to assess the feasibility of proposed flights for a new airline that wants to start running commercial passenger flights. This program assists in calculating the likely profitability of running a flight between a UK airport and an overseas airport, with the UK airport being either Liverpool John Lennon or Bournemouth International.


## Solution Design
* The functional aspect of the application has been abstracted into 7 main functions, defined in  **Program.cs**:
  * `void Menu()`
     *  This prompts the user to select from a string array of options by using the up arrow, down arrow, and enter key. The appropriate function is then called.
  * `void GenerateAirports()`
     *  This reads through `Airports.csv`, and then populates `Airports` with Airport objects with data from `Airports.csv`.
  * `void GeneratePlanes()`
     *  This reads through `Planes.csv`, and then populates `Planes` with Plane objects with data from `Planes.csv`.
  * `void EnterAirportDetails()`
     * This prompts the user to enter both the UK airport and overseas airport. The overseas airport is selected using the up arrow, down arrow, and enter key, from the `Airport` elements in `Airports`. Both the UK airport and overseas airport are added to the `ukAirport` and `overseasAirport` fields of the `FlightPlan` singleton.
  * `void EnterFlightDetails()`
     * This prompts the user to enter the type of plane to add to the `plane` field of the `FlightPlan` singleton.
     * The user is then prompted to enter the number of first-class seats, if there are any, and is prompted to do so again if the number is invalid. If valid, the field `firstClassNum` in the `FlightPlan` singleton is set to the number the user enters.
  * `void EnterPricePlanAndCalculateProfit()`
     * This first checks to see if the `ukAirport`, `overseasAirport`, and `plane` fields of the `FlightPlan` singleton are defined. If they aren't, the appropriate error messages are displayed to the console and the user is returned to the main menu.
     * If the checks pass, it then checks to see if the `maxFlightRange` of the `plane` field of the `FlightPlan` singleton is less than the `distFromBI` or `distFormJL`, depending on the value of `ukAirport` of the `FlightPlan` singleton, of the `overseasAirport` field of the `FlightPlan` singleton.
     * The user is then prompted to enter the price of both a first-class seat and a second-class seat. Error handling is then performed on this data, making the user enter it again if it is invalid.
     * The rest of the `FlightPlan`singleton is populated and finally printed to the console. The user is then returned to the menu.
  * `void ClearData()`
     * This calls the `ClearData()` method of the `FlightPlan` singleton, which resets all fields in `FlightPlan` to the defaults.
* The `Airport` class holds the following information, defined in **Airport.cs**:
  * `string code` - The 3-character code that represents the airport.
  * `string name` - The name of the airport.
  * `int distFromJL` - The distance of the airport from Liverpool John Lennon.
  * `int distFromBI` - The distance of the airport from Bournemouth International.
* The `Plane` class holds the following information, defined in **Plane.cs**:
  * `string type` - The type of plane.
  * `int runningCostPerSeatPer100Kilometres` - The running cost per seat per 100km.
  * `int maxFlightRange` - The maximum flight range of the plane.
  * `int capIfAllStandard` - The capacity of the plane, if all seats are standard class.
  * `int minFirstClass` - The minimum number of first-class seats, assuming that there are any.
* The following information is stored inside **FlightPlan.cs**, which is lost when the application ends:
  * `string ukAirport` - A string representing the selected UK airport.
  * `Airport overseasAirport` - An object of type `Airport` that holds the information for the currently selected overseas airport.
  * `int distance` - This holds the distance between the UK airport and overseas airport.
  * `Plane plane` - An object of type `Plane` that holds the information for the currently selected plane.
  * `int firstClassNum` - The number of first-class seats.
  * `int standardClassNum` - The number of standard-class seats.
  * `int firstClassPrice` - The price of each first-class seat.
  * `float flightCostPerSeat` - The cost of the flight per seat.
  * `float flightCost` - The total cost of the flight.
  * `float flightIncome` - The sum of all ticket prices from the flight.
  * `float profit` - The profit of the flight.



## Testing
Most of the user input is handled using the up arrow, down arrow, and enter key, where the user will select using the up and down arrows from a menu of options, and finalise the currently highlighted option with the enter key.
* The input method for the main menu:
```cs
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
```
User input is only allowed when an integer value is required from the user, for the assignment of `firstClassNum`, `standardClassPrice`, and `firstClassPrice`. The values that the user enters are validated, prompting the user to re-enter if they fail the validation steps.
* The error handling for `firstClassNum`:
```cs
while (true) // input number of first-class seats and error handling
{
    Console.Clear();
    if (!int.TryParse(UserInput("Input number of first-class seats: "), out firstClassNum))  
    { 
        ThrowError(); 
        continue; 
    }
    if ((firstClassNum > 0) && (firstClassNum < tempPlane.minFirstClass))  
    { 
        ThrowError("First-class seating must exceed minimum, if there are any.");
        continue;
    }
    else if (firstClassNum > (tempPlane.capIfAllStandard / 2))  
    {
        ThrowError("Exceeds maximum seating"); 
        continue;
    }
    break;
}
```
If the user does not enter a number, the following message is displayed, and the user is prompted to re-enter the number of first-class seats:
```
Invalid Input

Press Enter to continue...
```
If the user enters a number that does not exceed the minimum number of first-class seats, the following message is displayed, and the user is prompted to re-enter the number of first-class seats:
```
First-class seating must exceed minimum, if there are any.

Press Enter to continue...
```
If the user enters a number that exceeds the maximum seating of the currently selected plane type, the following message is displayed, and the user is prompted to re-enter the number of first-class seats:
```
Exceeds maximum seating

Press Enter to continue...
```
* The error handling for `standardClassPrice`:
```cs
if (!Int32.TryParse(UserInput("\nInput Standard-Class Seat Price:  £"), out int standardClassPrice)) 
{
    ReturnToMenu(); 
}
```
* The error handling for `firstClassPrice`:
```cs
if (!Int32.TryParse(UserInput("\nInput First-Class Seat Price:  £"), out int firstClassPrice)) 
{
    ReturnToMenu(); 
}
```
If the user's input for either `standardClassPrice` or `firstClassPrice` fails to be converted to an integer, the following message is displayed and the user is returned to the menu:
```
Invalid Input

Press Enter to return to menu...
```

* The error handling if the user attempts to call `EnterPricePlanAndCalculateProfit()` before entering required fields of `FlightPlan`:
```cs
int tempDistFromUk = 0;

string errors = null; 
bool ukAirportNull = false;
bool overseasAirportNull = false;
bool planeNull = false;
bool notWithinFlightRange = false;
bool missingFields = false;

if (FlightPlan.ukAirport == null) { ukAirportNull = true; missingFields = true; }
if (FlightPlan.overseasAirport == null) { overseasAirportNull = true;  missingFields = true; }
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

if (ukAirportNull) { errors += "\nUk Airport is missing"; }
if (overseasAirportNull) { errors += "\nOverseas Airport is missing"; }
if (planeNull) { errors += "\nAircraft type is missing"; }
if (notWithinFlightRange) { errors += "\nDistance exceeds maximum flight range"; }
if (missingFields) { ReturnToMenu(errors); }
```
If the distance between the UK airport and the overseas airport exceeds the `maxFlightRange` of the currently selected plane, the following message is displayed and the user is returned to the menu:
```
Distance exceeds maximum flight range

Press Enter to return to menu...
```
If any required fields are missing from the `FlightPlan` singleton, a message specifying the missing fields is displayed and the user is returned to the menu. The following message is displayed if all fields are missing:
```
UK Airport is missing
Overseas Airport is missing
Aircraft type is missing

Press Enter to return to menu...
```

## Potential Improvements 
* The design pattern used to display the currently selected option from the menu updates the entire console upon user input. This results in a momentary clear console. This could be replaced with code that simply redraws the console on the lines that need to be changed - the currently selected line, which should be deselected, and the line to be selected, which should be highlighted.
* Currently, the airport data and plane data parsed form `Airports.csv` and `Planes.csv` is entered into `Airports`, of type `List<Airport>`, and `Planes`, of type `List<Planes>`. This is heap-allocated and therefore causes some inefficiences. This could be improved by using arrays of type `Airport` and `Plane` that are declared globally with lengths set to the number of lines in `Airports.csv` and `Planes.csv`. These would be populated in `GenerateAirports()` and `GeneratePlanes()`.

## Building and running
* The user must have the .NET framework installed.
* In a terminal in the project directory, run:
>dotnet build
* Then run NEA2020.exe located in /bin/.
