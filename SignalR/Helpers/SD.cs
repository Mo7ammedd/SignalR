namespace SignalR.Helpers
{
    public static class SD
    {
        public const string Cloak = "cloak";
        public const string Stone = "stone";
        public const string Wand = "wand";

        public static Dictionary<string, int> DealthyHallowRace = new()
        {
            { Cloak, 0 },
            { Stone, 0 },
            { Wand, 0 }
        };
    }
}