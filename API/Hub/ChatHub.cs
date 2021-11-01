using Microsoft.AspNetCore.SignalR;

namespace API.Hub
{
    public class ChatHub : Hub<IChatClient>
    {
    }
}