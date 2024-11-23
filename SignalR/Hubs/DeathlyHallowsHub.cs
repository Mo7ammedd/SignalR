using Microsoft.AspNetCore.SignalR;
using SignalR.Helpers;

namespace SignalR.Hubs;


    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            Console.WriteLine($"Cloak: {SD.DealthyHallowRace[SD.Cloak]}, Stone: {SD.DealthyHallowRace[SD.Stone]}, Wand: {SD.DealthyHallowRace[SD.Wand]}");
            return SD.DealthyHallowRace;
        }
    }

    
