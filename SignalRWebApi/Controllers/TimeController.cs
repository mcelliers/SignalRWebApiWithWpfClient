using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRWebApi.Hubs;

namespace SignalRWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeController : ControllerBase
{

    private readonly IHubContext<NotificationsHub> _hubContext; 
    public TimeController(IHubContext<NotificationsHub> hubContext)
    {
        _hubContext = hubContext;
    }
    [HttpGet]
    public async Task<IResult> GetTime()
    {

        var message = DateTime.Now.ToString();
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Marius", message);
        //await _hubContext.Clients.All.SendAsync("SendMessage", "Marius", message);
        return Results.Ok(message);
    }

}
