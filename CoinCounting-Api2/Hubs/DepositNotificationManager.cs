using CoinCounting_Api;
using Microsoft.AspNetCore.SignalR;

namespace CoinCounting_Api2.Hubs
{
    public class DepositNotificationManager
    {
        private readonly IHubContext<DepositHub> DepositContext;

        public DepositNotificationManager(IHubContext<DepositHub> depositContext)
        {
            this.DepositContext = depositContext ?? throw new ArgumentNullException(nameof(depositContext));
        }

        public Task BroadCast(CoinDepositDto dto, CancellationToken token = default)
        {
            return DepositContext.Clients.All.SendAsync("Broadcast", dto, token);
        }
    }
}
