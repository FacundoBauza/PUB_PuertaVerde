using Microsoft.AspNetCore.SignalR;

namespace SignalR
{
    public sealed class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} joined the conversation");
        }
    }
}