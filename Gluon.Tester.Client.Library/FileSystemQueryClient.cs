using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;

namespace Gluon.Tester.Client.Library
{
    public class FileSystemQueryClient : RequestResponseClientBase<FileSystemQueryRqst, FileSystemQueryRspn>
    {
        public FileSystemQueryClient(string instanceId, string subscriptionChannel) : base(instanceId, subscriptionChannel)
        { }
    }
}
