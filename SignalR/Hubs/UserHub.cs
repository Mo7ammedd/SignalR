using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs;

public class UserHub : Hub
{
    public static int ViewersCount { get; set; }=0;

    public static int TotalUsers { get; set; }=0;

    public override Task OnConnectedAsync()
    {
        TotalUsers++;
        Clients.All.SendAsync("UpdateTotalUsers", TotalUsers).GetAwaiter().GetResult();
        return base.OnConnectedAsync(); 
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        TotalUsers--;
         Clients.All.SendAsync("UpdateTotalUsers", TotalUsers).GetAwaiter().GetResult();
        return base.OnDisconnectedAsync(exception);
    }

    public async Task NewWindowLoaded()
    {
        ViewersCount++;
        await Clients.All.SendAsync("UpdateTotalView", ViewersCount);
    }
}