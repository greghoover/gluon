using Gluon.Relay.Contracts;
using System;

namespace Gluon.Tester.Contracts
{
    public class FileSystemQueryRspn : RelayResponseBase<FileSystemQueryRqst>
    {
        public string ResponseString { get; set; }

        public FileSystemQueryRspn(FileSystemQueryRqst request, string responseString) : base(request)
        {
            this.ResponseString = responseString;
        }

        public override string ToString() =>
            $"Request : {this.Request}{Environment.NewLine}Response: {this.ResponseString}.";
    }
}
