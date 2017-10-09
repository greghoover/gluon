using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public class MessageHub : Hub
    {
        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.All.InvokeAsync("Send", $"{Context.ConnectionId} connected");
        //}
        //public override async Task OnDisconnectedAsync(Exception ex)
        //{
        //    await Clients.All.InvokeAsync("Send", $"{Context.ConnectionId} disconnected");
        //}

        //public Task SendToGroup(string groupName, string message)
        //{
        //    return Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId}@{groupName}: {message}");
        //}
        //public async Task JoinGroup(string groupName)
        //{
        //    await Groups.AddAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        //}
        //public async Task LeaveGroup(string groupName)
        //{
        //    await Groups.RemoveAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} left {groupName}");
        //}


        //public Task Echo(string message)
        //{
        //    return Clients.Client(Context.ConnectionId).InvokeAsync("Send", $"{Context.ConnectionId}: {message}");
        //}

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

        //public Task DoWork(string methodName = "DoWork", string data = "(empty)")
        //{
        //    var ids = new List<string>();
        //    ids.Add(Context.ConnectionId);
        //    var client = Clients.AllExcept(ids);
        //    return client.InvokeAsync(methodName, data);
        //    //return Clients.All.InvokeAsync(methodName, data);
        //}

        //public Task Call(string methodName = "DoWork", string data = "(empty)")
        //{
        //    return Clients.All.InvokeAsync(methodName, data);
        //}

        //public Task Send(string message)
        //{
        //    var ids = new List<string>();
        //    ids.Add(Context.ConnectionId);
        //    var client = Clients.AllExcept(ids);
        //    return client.InvokeAsync("DoWork", message);
        //    //return Clients.All.InvokeAsync("Send", $"{Context.ConnectionId} {message}");
        //}
    }
}
