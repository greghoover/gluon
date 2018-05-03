using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    [ProtoContract]
    public class FileSystemQueryResponse : ApplicationResponseMessage
    {
        [ProtoMember(1)]
        public FileSystemQueryRequest Request
        {
            get { return this.ApplicationRequestMessage as FileSystemQueryRequest; }
        }
        [ProtoMember(2)]
        public string ResponseString { get; set; }

        private FileSystemQueryResponse() { }
        public FileSystemQueryResponse(FileSystemQueryRequest request, string responseString)
        {
            this.ApplicationRequestMessage = request;
            this.ResponseString = responseString;
        }

        public override string ToString() =>
            $"Request: [{this.Request}] Response: [{this.ResponseString}]";
    }
}
