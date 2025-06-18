using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TapNGo.DAL.Models;

namespace TapNGo.Hubs.SignalR
{
    public class OrderHub : Hub
    {
        public async Task NotifyNewOrder(int orderId)
        {
            await Clients.All.SendAsync("ReceiveOrder", orderId);
        }
    }
}
