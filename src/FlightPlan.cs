namespace NEA2020
{
    static class FlightPlan
    {
        public static string ukAirport;
        public static Airport overseasAirport;
        public static int distance;
        public static Plane plane;
        public static int firstClassNum;
        public static int standardClassNum;
        public static int standardClassPrice;
        public static int firstClassPrice;
        public static float flightCostPerSeat;
        public static float flightCost;
        public static float flightIncome;
        public static float profit;

        public static void ClearData()
        {
            ukAirport = null;
            overseasAirport = null;
            distance = 0;
            plane = null;
            firstClassNum = 0;
            standardClassNum = 0;
            standardClassPrice = 0;
            firstClassPrice = 0;
            flightCostPerSeat = 0;
            flightCost = 0;
            flightIncome = 0;
            profit = 0;
        }
    }
}