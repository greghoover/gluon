using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    [ProtoContract]
    public class FileSystemQueryResponse : ProxyMessage
    {
        [ProtoMember(1)]
        public FileSystemQueryRequest Request { get; set; }
        [ProtoMember(2)]
        public string ResponseString { get; set; }

        private FileSystemQueryResponse() { }
        public FileSystemQueryResponse(FileSystemQueryRequest request, string responseString)
        {
            this.ResponseString = responseString;
        }

        public override string ToString() =>
            $"Request: [{this.Request}] Response: [{this.ResponseString}]";
    }
}
