using ProtoBuf;
using System;

namespace hbar.Contract.FileSystemQuery
{
    [ProtoContract]
    public class FileSystemQueryResponse
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
            $"Request : {this.Request}{Environment.NewLine}Response: {this.ResponseString}.";
    }
}
