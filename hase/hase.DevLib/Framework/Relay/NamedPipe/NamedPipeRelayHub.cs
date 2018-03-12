using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayHub<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        /// <summary>
        /// e.g. FileSystemQueryProxy
        /// </summary>
        public string ProxyPipeName { get; private set; }
        /// <summary>
        /// e.g. FileSystemQueryService
        /// </summary>
        public string ServicePipeName { get; private set; }

        private NamedPipeServerStream _proxyPipe;
        private NamedPipeServerStream _servicePipe;

        public NamedPipeRelayHub(string servicePipeName, string proxyPipeName)
        {
            this.ProxyPipeName = proxyPipeName;
            this.ServicePipeName = servicePipeName;

            _proxyPipe = new NamedPipeServerStream(this.ProxyPipeName, PipeDirection.InOut);
            _servicePipe = new NamedPipeServerStream(this.ServicePipeName, PipeDirection.InOut);
        }

        public void Start()
        {
            ListenForServiceConnection();
            while (true)
            {
                ProcessProxyRequest();
            }
        }
        public void Stop()
        {
        }

        private void ListenForServiceConnection()
        {
            Console.WriteLine($"nprs:Listening for {this.ServicePipeName} connection.");
            _servicePipe.WaitForConnection();
            Console.WriteLine($"nprs:{this.ServicePipeName} connected.");
        }

        private void ProcessProxyRequest()
        {
            try
            {
                if (!_proxyPipe.IsConnected)
                {
                    Console.WriteLine($"nprs:Listening for {this.ProxyPipeName} connection.");
                    _proxyPipe.WaitForConnection();
                    Console.WriteLine($"nprs:{this.ProxyPipeName} connected.");
                }

                //Console.WriteLine($"nprs:Waiting to receive {_proxyPipeName} request.");
                var request = Serializer.DeserializeWithLengthPrefix<TRequest>(_proxyPipe, PrefixStyle.Base128);
                Console.WriteLine($"nprs:Received {this.ProxyPipeName} request: {request}.");

                TResponse response = null;
                if (_servicePipe != null && _servicePipe.IsConnected)
                {
                    Console.WriteLine($"nprs:Forwarding {this.ServicePipeName} request: {request}.");
                    Serializer.SerializeWithLengthPrefix(_servicePipe, request, PrefixStyle.Base128);
                    //Console.WriteLine($"nprs:Forwared {_servicePipeName} request.");

                    //Console.WriteLine($"nprs:Waiting to receive {_servicePipeName} response.");
                    response = Serializer.DeserializeWithLengthPrefix<TResponse>(_servicePipe, PrefixStyle.Base128);
                    Console.WriteLine($"nprs:Received {this.ServicePipeName} response: {response}.");
                }


                //Console.WriteLine($"nprs:Returning {_proxyPipeName} response.");
                Serializer.SerializeWithLengthPrefix(_proxyPipe, response, PrefixStyle.Base128);
                Console.WriteLine($"nprs:Returned {this.ProxyPipeName} response.");

                _proxyPipe.Disconnect();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
