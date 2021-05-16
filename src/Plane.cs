namespace NEA2020
{
    class Plane
    {
        public string type;
        public int runningCostPerSeatPer100Kilometres;
        public int maxFlightRange;
        public int capIfAllStandard;
        public int minFirstClass;

        public Plane(string type, int runningCostPerSeatPer100Kilometres, int maxFlightRange, int capIfAllStandard, int minFirstClass)
        {
            this.type = type;
            this.runningCostPerSeatPer100Kilometres = runningCostPerSeatPer100Kilometres;
            this.maxFlightRange = maxFlightRange;
            this.capIfAllStandard = capIfAllStandard;
            this.minFirstClass = minFirstClass;
        }

        public Plane() { }


    }
}