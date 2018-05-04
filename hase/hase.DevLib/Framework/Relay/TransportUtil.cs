using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using hase.DevLib.Framework.Contract;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace hase.DevLib.Framework.Relay
{
    public static class TransportUtil
    {
        //public static string TransportRequestIdHeader => "pragma";
        //public static string TransportResponseIdHeader => "pragma";
        //public static string TransportRequestIdHeader => "x-Request-ID";
        //public static string TransportResponseIdHeader => "x-Response-ID";
        public static string TransportRequestIdHeader => "request-Id";
        public static string TransportResponseIdHeader => "response-Id";
        public static string TransportChannelHeader => "fromChannel";
        public static string TransportDateHeader => "date";

        //http.Content = new StringContent(JsonConvert.SerializeObject(request));

        public static HttpRequestMessageWrapperEx ToTransportRequest(this AppRequestMessage request)
        {
            var http = new HttpRequestMessage(HttpMethod.Get, new Uri(@"http://www.google.com"));
            http.Headers.Date = request.Headers.CreatedOn;
            http.Headers.Add(TransportChannelHeader, request.Headers.SourceChannel);
            // The following line of code is necessary prior to assigning a 'pragma' http header.
            //http.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true };
            http.Headers.Add(TransportRequestIdHeader, request.Headers.MessageId);

            var content = JsonConvert.SerializeObject(request);
            var wrapper = new HttpRequestMessageWrapperEx(http, content);

            return wrapper;
        }

        public static string GetRequestId(this HttpRequestMessageWrapper wrapper)
        {
            return wrapper.Headers[TransportRequestIdHeader].FirstOrDefault();
        }

        public static DateTimeOffset? GetCreatedOn(this HttpMessageWrapper wrapper)
        {
            var date = wrapper.Headers[TransportDateHeader].FirstOrDefault();
            var createdOn = default(DateTimeOffset);
            DateTimeOffset.TryParse(date, out createdOn);
            return createdOn;
        }
        public static string GetSourceChannel(this HttpMessageWrapper wrapper)
        {
            return wrapper.Headers[TransportChannelHeader].FirstOrDefault();
        }

        public static string GetResponseId(this HttpResponseMessageWrapper wrapper)
        {
            return wrapper.Headers[TransportResponseIdHeader].FirstOrDefault();
        }


        public static TRequest ToAppRequestMessage<TRequest>(this HttpRequestMessageWrapperEx wrapper) where TRequest : AppRequestMessage
        {
            return JsonConvert.DeserializeObject<TRequest>(wrapper.Content);
        }
        public static TResponse ToAppResponseMessage<TResponse>(this HttpResponseMessageWrapperEx wrapper) where TResponse : AppResponseMessage
        {
            return JsonConvert.DeserializeObject<TResponse>(wrapper.Content);
        }

        public static HttpResponseMessageWrapperEx ToTransportResponse(this AppResponseMessage response)
        {
            var http = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            http.Headers.Date = response.Headers.CreatedOn;
            http.Headers.Add(TransportChannelHeader, response.Headers.SourceChannel);
            // The following line of code is necessary prior to assigning a 'pragma' http header.
            //http.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true };
            http.Headers.Add(TransportResponseIdHeader, response.Headers.MessageId);

            var content = JsonConvert.SerializeObject(response);
            var wrapper = new HttpResponseMessageWrapperEx(http, content);

            // Maybe pass this in so we don't have to do it again.
            //wrapper.RequestWrapper = response.AppRequestMessage.ToTransportRequest();

            return wrapper;
        }

        public static string GetFirstHeaderValue(this HttpHeaders headers, string headerName)
        {
            IEnumerable<string> values;
            if (headers.TryGetValues(headerName, out values))
            {
                foreach (string vlu in values)
                {
                    return vlu;
                }
            }
            return default(string);
        }
    }
}
