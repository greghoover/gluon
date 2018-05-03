using Microsoft.Rest;
using System.Net.Http;

namespace hase.DevLib.Framework.Relay
{
    public class HttpRequestMessageWrapperEx : HttpRequestMessageWrapper
    {
        public HttpRequestMessageWrapperEx() : base(new HttpRequestMessage(), null) { }
        public HttpRequestMessageWrapperEx(HttpRequestMessage httpRequest, string content) : base(httpRequest, content) { }
    }
}
