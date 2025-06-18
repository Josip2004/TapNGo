using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace TapNGoMVC.Controllers
{
    public class WaiterController : Controller
    {
        private HubConnection _hubConnection;

        public IActionResult CallWaiter(string note)
        {
            if(string.IsNullOrEmpty(note))
            {
                note = "";
            }
            int? tableNum = HttpContext.Session.GetInt32("TableNumber");
            TempData["Error"] = null;
            if (!tableNum.HasValue)
            {
                TempData["Error"] = "Oprostite, ovu funkciju možete koristiti samo dok ste u kafiću.";
                return RedirectToAction("Index", "Menu");
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5235/waiterHub")
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.StartAsync().GetAwaiter().GetResult();


            _hubConnection.InvokeAsync("CallWaiter", tableNum, note).GetAwaiter().GetResult();

            TempData["CalledWaiter"] = "Konobar je obaviješten.";
            return RedirectToAction("Index", "Menu");

        }
    }
}
