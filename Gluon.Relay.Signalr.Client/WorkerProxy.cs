//
using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public class WorkerProxy
    {
        ICommunicationClient _commClient = null;

        public WorkerProxy() : this(null) { }
        public WorkerProxy(ICommunicationClient hubClient)
        {
            _commClient = hubClient;
        }

        public TResponse Invoke<TRequest, TResponse>(string methodName, TRequest arg0)
        {
            //Task<object> t2 = _commClient.InvokeAsync("Send", typeof(string), CancellationToken.None, "Hello");
            //object o2 = t2.Result;
            //string s2 = o2.ToString();

            //_commClient.InvokeAsync(methodName, methodName, arg0).Wait();
            _commClient.InvokeAsync(methodName, arg0).Wait();
            //object o = t.Result;
            //return (TResponse)t.Result;
            return default(TResponse);
        }

        public void HubClientInit()
        {

            //client.On<string>("Send", data =>
            //{
            //    Console.WriteLine($"Received: {data}");
            //});
            //client.On<string>("Fred2", data =>
            //{
            //    Console.WriteLine($"Received: {data}");
            //});

            //client.StartAsync().Wait();

            //client.InvokeAsync("Send", "Hello").Wait();

            //client.InvokeAsync("Fred1", "fred1");
        }
    }
}
