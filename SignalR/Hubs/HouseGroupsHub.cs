using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs;

public class HouseGroupsHub : Hub
{
    private static List<string> GroupsJoined { get;set; } = new List<string>();
    
    public async Task JoinHouse(string houseName)
    {
        if (!GroupsJoined.Contains( Context.ConnectionId+"_"+houseName))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
            GroupsJoined.Add(Context.ConnectionId+"_"+houseName);
        }
       
    }
    public async Task LeaveHouse(string houseName)
    {
        if (GroupsJoined.Contains( Context.ConnectionId+"_"+houseName))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
            GroupsJoined.Remove(Context.ConnectionId+"_"+houseName);
        }
    }
    
}