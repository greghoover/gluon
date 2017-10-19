//using Gluon.Relay.Contracts;
//using Gluon.Relay.Signalr.Client;
//using Gluon.Tester.Contracts;

//namespace Gluon.Tester.Client.Library
//{
//    public class FileSystemQueryClient : AppServiceClient, IRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>
//    {
//        public FileSystemQueryClient(string instanceId, string subscriptionChannel) : base(instanceId, subscriptionChannel) { }

//        public FileSystemQueryRspn RequestResponse(FileSystemQueryRqst request)
//        {
//            return RelayRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>(request);
//        }
//    }
//}
