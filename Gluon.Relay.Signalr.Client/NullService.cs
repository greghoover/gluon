using Gluon.Relay.Contracts;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public class NullService : IServiceType
    {
        public string Execute(ICommunicationClient hub, string inputMsg)
        {
            throw new NotImplementedException();
        }

        public object Execute(ICommunicationClient hub, object inputMsg)
        {
            throw new NotImplementedException();
        }
    }
}

