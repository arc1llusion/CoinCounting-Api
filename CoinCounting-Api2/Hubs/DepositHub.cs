using Microsoft.AspNetCore.SignalR;

namespace CoinCounting_Api2.Hubs
{
    public class DepositHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }
        public Task SendDeposit(string message)
        {
            return Clients.All.SendAsync("SendDeposit", message);
        }
    }
}
