﻿using Gluon.Relay.Contracts;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public class NullService : IServiceType
    {
        public void Execute(IRemoteMethodInvoker proxy, object request)
        {
            throw new NotImplementedException();
        }
    }
}

