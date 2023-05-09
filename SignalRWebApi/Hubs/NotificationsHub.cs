using Microsoft.AspNetCore.SignalR;
using SignalRWebApi.Models;

namespace SignalRWebApi.Hubs;

public class NotificationsHub : Hub
{
    public NotificationsHub()
    {
        
    }

    public async Task SendMessage(string user,string message)
    {
       await Clients.All.SendAsync(method: "ReceiveMessage", user, message);
    }

    public async Task SendNotification(string user, NotificationsModel message)
    {
        await Clients.All.SendAsync(method: "ReceiveNotification", user, message);
    }



}
