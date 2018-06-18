using Microsoft.Rest;
using System.Net.Http;

namespace hase.DevLib.Framework.Relay.Contract
{
    /// <summary>
    /// Extended to add a paramaterless constructor for Json de-serialization.
    /// </summary>
    public class HttpRequestMessageWrapperEx : HttpRequestMessageWrapper
    {
        public HttpRequestMessageWrapperEx() : this(new HttpRequestMessage(), null) { }
        public HttpRequestMessageWrapperEx(HttpRequestMessage httpRequest) : this(httpRequest, null) { }
        public HttpRequestMessageWrapperEx(HttpRequestMessage httpRequest, string content) : base(httpRequest, content) { }
    }
}
