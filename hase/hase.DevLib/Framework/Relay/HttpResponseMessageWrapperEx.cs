using Microsoft.Rest;
using System.Net.Http;

namespace hase.DevLib.Framework.Relay
{
    public class HttpResponseMessageWrapperEx : HttpResponseMessageWrapper
    {
        public HttpRequestMessageWrapper RequestWrapper { get; set; }

        public HttpResponseMessageWrapperEx() : base(new HttpResponseMessage(), null) { }

        public HttpResponseMessageWrapperEx(HttpResponseMessage httpResponse, string content) : base(httpResponse, content)
        {
        }
        //public HttpResponseMessageWrapperEx(HttpResponseMessage httpResponse, string content, HttpRequestMessageWrapper requestWrapper) : base(httpResponse, content)
        //{
        //    this.RequestWrapper = requestWrapper;
        //}
    }
}
