using System;
//
using Gluon.Relay.Contracts;
//using Gluon.Tester.Contracts;

namespace Gluon.Tester.Server.Library
{
    public class Worker //: IWorker
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

