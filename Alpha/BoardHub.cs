using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace signalrBoard
{
  public class BoardHub : Hub
  {
    public async Task SendMessage(string user, string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
  }
}