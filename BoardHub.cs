using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRBoard
{
  public class BoardHub : Hub
  {
    public async Task SendMessage(int colIndex, string author, string description)
    {
      await Clients.All.SendAsync("ReceiveMessage", colIndex, author, description);
    }
  }
}