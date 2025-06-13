using EventAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace EventAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Format timestamp
            var timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // Send message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message, timestamp);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        }
    }
}
