using hase.DevLib.Framework.Contract;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Contract
{
    public static class TransportUtil
    {
        public static string CustomRequestHeaderPrefix => "custom-request-";
        public static string CustomResponseHeaderPrefix => "custom-response-";

        //public static string RequestIdHeader => "pragma";
        public static string RequestIdHeader => "request-Id";
        public static string RequestDateHeader => "date";
        public static string RequestChannelHeader => "from-Channel";

        //public static string ResponseIdHeader => "pragma";
        public static string ResponseIdHeader => "response-Id";
        public static string ResponseDateHeader => "date";
        public static string ResponseChannelHeader => "from-Channel";

        public async static Task<HttpRequestMessageWrapperEx> ToTransportRequestAsync(this AppRequestMessage request)
        {
            // Could interact with the wrapper directly, but trying to use
            // http message as much as possible so that if we want to push
            // the wrapper contents into an http message, would prob have better result.
            var http = new HttpRequestMessage();
            http.Content = new StringContent(JsonConvert.SerializeObject(request));

            http.Headers.Add(RequestIdHeader, request.Headers.MessageId);
            http.Headers.Date = request.Headers.CreatedOn;
            //http.Headers.Add(RequestDateHeader, request.Headers.CreatedOn?.ToString());
            http.Headers.Add(RequestChannelHeader, request.Headers.SourceChannel);

            foreach (var customHeader in request.Headers.Custom)
            {
                http.Headers.Add($"{CustomRequestHeaderPrefix}{customHeader.Key}", customHeader.Value);
            }

            //var content = http.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var content = await http.Content.ReadAsStringAsync();
            var wrapper = new HttpRequestMessageWrapperEx(http, content);

            return wrapper;
        }
        public static TRequest ToAppRequestMessage<TRequest>(this HttpRequestMessageWrapperEx wrapper) where TRequest : AppRequestMessage
        {
            return JsonConvert.DeserializeObject<TRequest>(wrapper.Content);
        }
        public static AppRequestMessage ToAppRequestMessage(this HttpRequestMessageWrapperEx wrapper, Type requestType)
        {
            return (AppRequestMessage)JsonConvert.DeserializeObject(wrapper.Content, requestType);
        }

        public async static Task<HttpResponseMessageWrapperEx> ToTransportResponseAsync(this AppResponseMessage response)
        {
            // Could interact with the wrapper directly, but trying to use
            // http message as much as possible so that if we want to push
            // the wrapper contents into an http message, would prob have better result.
            var http = new HttpResponseMessage();
            http.Content = new StringContent(JsonConvert.SerializeObject(response));

            http.Headers.Add(ResponseIdHeader, response.Headers.MessageId);
            http.Headers.Date = response.Headers.CreatedOn;
            //http.Headers.Add(ResponseDateHeader, response.Headers.CreatedOn?.ToString());
            http.Headers.Add(ResponseChannelHeader, response.Headers.SourceChannel);

            foreach (var customHeader in response.Headers.Custom)
            {
                http.Headers.Add($"{CustomResponseHeaderPrefix}{customHeader.Key}", customHeader.Value);
            }

            var content = await http.Content.ReadAsStringAsync();
            var wrapper = new HttpResponseMessageWrapperEx(http, content);

            // TODO: Should probably pass this in to forego the overhead of performing it twice.
            wrapper.RequestWrapper = await response.AppRequestMessage.ToTransportRequestAsync();

            return wrapper;
        }
        public static TResponse ToAppResponseMessage<TResponse>(this HttpResponseMessageWrapperEx wrapper) where TResponse : AppResponseMessage
        {
            return JsonConvert.DeserializeObject<TResponse>(wrapper.Content);
        }


        public static string GetRequestId(this HttpRequestMessageWrapper wrapper)
        {
            return wrapper.Headers[RequestIdHeader].FirstOrDefault();
        }
        public static DateTimeOffset? GetCreatedOn(this HttpRequestMessageWrapper wrapper)
        {
            var date = wrapper.Headers[RequestDateHeader].FirstOrDefault();
            var createdOn = default(DateTimeOffset);
            DateTimeOffset.TryParse(date, out createdOn);
            return createdOn;
        }
        public static string GetSourceChannel(this HttpRequestMessageWrapper wrapper)
        {
            return wrapper.Headers[RequestChannelHeader].FirstOrDefault();
        }
        public static string GetCustomRequestHeaderKey(string headerName)
        {
            return $"{CustomRequestHeaderPrefix}{headerName}";
        }
        public static string GetCustomRequestHeader(this HttpRequestMessageWrapper wrapper, string headerName)
        {
            return wrapper.Headers[GetCustomRequestHeaderKey(headerName)].FirstOrDefault();
        }


        public static string GetResponseId(this HttpResponseMessageWrapper wrapper)
        {
            return wrapper.Headers[ResponseIdHeader].FirstOrDefault();
        }
        public static DateTimeOffset? GetCreatedOn(this HttpResponseMessageWrapper wrapper)
        {
            var date = wrapper.Headers[ResponseDateHeader].FirstOrDefault();
            var createdOn = default(DateTimeOffset);
            DateTimeOffset.TryParse(date, out createdOn);
            return createdOn;
        }
        public static string GetSourceChannel(this HttpResponseMessageWrapper wrapper)
        {
            return wrapper.Headers[ResponseChannelHeader].FirstOrDefault();
        }
        public static string GetCustomResponseHeaderKey(string headerName)
        {
            return $"{CustomResponseHeaderPrefix}{headerName}";
        }
        public static string GetCustomResponseHeader(this HttpResponseMessageWrapper wrapper, string headerName)
        {
            return wrapper.Headers[GetCustomResponseHeaderKey(headerName)].FirstOrDefault();
        }
    }
}
