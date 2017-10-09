using System;
//
using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public class NullService : IServiceType
    {
        public string Execute(string inputMsg)
        {
            throw new NotImplementedException();
        }

        public object Execute(object inputMsg)
        {
            throw new NotImplementedException();
        }
    }
}

