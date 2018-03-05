using hbar.Contract.FileSystemQuery;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hbar.Relay
{
    public class NamedPipeRelay
    {
        private static readonly string _proxyPipeName = "FileSystemQueryProxy";
        private static readonly string _servicePipeName = "FileSystemQueryService";

        private NamedPipeServerStream _proxyPipe;
        private NamedPipeServerStream _servicePipe;

        public NamedPipeRelay()
        {
            _proxyPipe = new NamedPipeServerStream(_proxyPipeName, PipeDirection.InOut);
            _servicePipe = new NamedPipeServerStream(_servicePipeName, PipeDirection.InOut);
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
            Console.WriteLine($"nprs:Listening for {_servicePipeName} connection.");
            _servicePipe.WaitForConnection();
            Console.WriteLine($"nprs:{_servicePipeName} connected.");
        }

        private void ProcessProxyRequest()
        {
            try
            {
                if (!_proxyPipe.IsConnected)
                {
                    Console.WriteLine($"nprs:Listening for {_proxyPipeName} connection.");
                    _proxyPipe.WaitForConnection();
                    Console.WriteLine($"nprs:{_proxyPipeName} connected.");
                }

                //Console.WriteLine($"nprs:Waiting to receive {_proxyPipeName} request.");
                var request = Serializer.DeserializeWithLengthPrefix<FileSystemQueryRequest>(_proxyPipe, PrefixStyle.Base128);
                Console.WriteLine($"nprs:Received {_proxyPipeName} request: {request}.");

                FileSystemQueryResponse response = null;
                if (_servicePipe != null && _servicePipe.IsConnected)
                {
                    Console.WriteLine($"nprs:Forwarding {_servicePipeName} request: {request}.");
                    Serializer.SerializeWithLengthPrefix(_servicePipe, request, PrefixStyle.Base128);
                    //Console.WriteLine($"nprs:Forwared {_servicePipeName} request.");

                    //Console.WriteLine($"nprs:Waiting to receive {_servicePipeName} response.");
                    response = Serializer.DeserializeWithLengthPrefix<FileSystemQueryResponse>(_servicePipe, PrefixStyle.Base128);
                    Console.WriteLine($"nprs:Received {_servicePipeName} response: {response}.");
                }


                //Console.WriteLine($"nprs:Returning {_proxyPipeName} response.");
                Serializer.SerializeWithLengthPrefix(_proxyPipe, response, PrefixStyle.Base128);
                Console.WriteLine($"nprs:Returned {_proxyPipeName} response.");

                _proxyPipe.Disconnect();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
