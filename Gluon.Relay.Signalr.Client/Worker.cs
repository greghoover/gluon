using System;
//
using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public interface IWorker
    {
        string DoWork(string commandName, string commandData);
    }

    public class Worker : IWorker
    {
        ICommunicationClient _commClient = null;

        public Worker() : this(null) { }
        public Worker(ICommunicationClient hubClient)
        {
            _commClient = hubClient;
        }

        public string DoWork(string commandName, string commandData)
        {
            var msg = $"Command [{commandName}::{commandData}] completed.";
            Console.WriteLine(msg);
            return msg;
        }
    }
}

