using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.AppServices.FileSystemQuery.Contract
{
	[ProtoContract]
	public class FileSystemQueryResponse : AppResponseMessage
	{
		[ProtoMember(1)]
		public string ResponseString { get; set; }

		private FileSystemQueryResponse() { }
		public FileSystemQueryResponse(FileSystemQueryRequest request, string responseString) : base(request)
		{
			this.ResponseString = responseString;
		}

		public override string ToString() =>
			$"Request: [{this.AppRequestMessage}] Response: [{this.ResponseString}]";
	}
}
