using Gluon.Relay.Contracts;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public class NullService : IServiceType
    {
        public object Execute(ICommunicationClient hub, object request)
        {
            throw new NotImplementedException();
        }
    }
}

