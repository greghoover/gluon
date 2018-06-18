using Microsoft.Rest;
using System.Net.Http;

namespace hase.DevLib.Framework.Relay.Contract
{
    /// <summary>
    /// Extended to add a paramaterless constructor for Json de-serialization, and for a property to store the original request.
    /// </summary>
    public class HttpResponseMessageWrapperEx : HttpResponseMessageWrapper
    {
        public HttpRequestMessageWrapperEx RequestWrapper { get; set; }

        public HttpResponseMessageWrapperEx() : this(new HttpResponseMessage(), null) { }
        public HttpResponseMessageWrapperEx(HttpResponseMessage httpResponse) : this(httpResponse, null) { }
        public HttpResponseMessageWrapperEx(HttpResponseMessage httpResponse, string content) : this(httpResponse, content, null) { }
        public HttpResponseMessageWrapperEx(HttpResponseMessage httpResponse, string content, HttpRequestMessageWrapperEx requestWrapper) : base(httpResponse, content)
        {
            this.RequestWrapper = requestWrapper;
        }
    }
}
