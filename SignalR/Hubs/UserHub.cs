using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs;

public class UserHub : Hub
{
    public static int ViewersCount { get; set; }=0;
    public async Task NewWindowLoaded()
    {
        ViewersCount++;
        await Clients.All.SendAsync("UpdateTotalView", ViewersCount);
    }
}