namespace NEA2020
{
    class Airport
    {
        public string code;
        public string name;
        public int distFromJL;
        public int distFromBI;

        public Airport(string code, string name, int distFromJL, int distFromBI)
        {
            this.code = code;
            this.name = name;
            this.distFromJL = distFromJL;
            this.distFromBI = distFromBI;
        }

        public Airport() { }
    }
}