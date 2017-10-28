using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Sockets.Http.Features;
using System;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    //[Authorize]
    public partial class RelayServer : Hub, IRelayServer
    {
        static RelayServer()
        {
            InitRequestResponseCache();
            InitClientLookup();
        }

        public override Task OnConnectedAsync()
        {
            var msg = new LogonMsg
            {
                ConnectionId = this.Context.ConnectionId,
                ClientId = this.GetHttpContextItem<string>(ClientIdTypeEnum.ClientId.ToString()),
                UserId = this.GetHttpContextItem<string>(ClientIdTypeEnum.UserId.ToString()),
            };

            this.Logon(msg);

            return base.OnConnectedAsync();
        }
        private T GetHttpContextItem<T>(string key)
        {
            var httpContext = Context.Connection.Features.Get<IHttpContextFeature>().HttpContext;
            object vlu;
            if (httpContext.Items.TryGetValue(key, out vlu))
                return (T)vlu;
            else
                return default(T);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var msg = new LogoffMsg
            {
                ClientIdentifier = new ClientIdentifier
                {
                    ClientIdType = ClientIdTypeEnum.ConnectionId,
                    ClientIdValue = Context.ConnectionId,
                }
            };

            this.Logoff(msg);

            return base.OnDisconnectedAsync(exception);
        }

    }
}
