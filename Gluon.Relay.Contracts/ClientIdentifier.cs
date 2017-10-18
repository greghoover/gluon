using System;
using System.Collections.Generic;
using System.Text;

namespace Gluon.Relay.Contracts
{
    public class ClientIdentifier
    {
        public ClientSpecEnum? ClientIdentifierType { get; set; }
        public string ClientIdentifierValue { get; set; }
    }
}
