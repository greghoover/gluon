using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public class MessageHub : Hub
    {
        public Task DoWork(object data)
        {
            if (data == null)
                data = "(null)";

            //var ids = new List<string>();
            //ids.Add(Context.ConnectionId);
            //var client = Clients.AllExcept(ids);
            //return client.InvokeAsync("DoWork", data);

            return Clients.All.InvokeAsync("DoWork", data);
        }
    }
}
