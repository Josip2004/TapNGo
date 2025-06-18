using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TapNGo.DAL.Models;

namespace TapNGo.Hubs.SignalR
{
    public class WaiterHub : Hub
    {
        public async Task CallWaiter(int tableId, string note)
        {
            await Clients.All.SendAsync("CalledWaiter", tableId, note);
        }
    }
}
