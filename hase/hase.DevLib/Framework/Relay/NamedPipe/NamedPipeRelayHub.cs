using ProtoBuf;
using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public NamedPipeRelayHub(string servicePipeName, string proxyPipeName)
        {
            this.ProxyPipeName = proxyPipeName;
            this.ServicePipeName = servicePipeName;

            _proxyPipe = new NamedPipeServerStream(this.ProxyPipeName, PipeDirection.InOut);
            _servicePipe = new NamedPipeServerStream(this.ServicePipeName, PipeDirection.InOut);
        }

        public async Task StartAsync()
        {
            var ct = _cts.Token;

            await ListenForServiceConnectionAsync(ct);
            while (!ct.IsCancellationRequested)
            {
                await ProcessProxyRequestAsync(ct);
            }
        }
        public async Task StopAsync()
        {
            _cts.Cancel();
            await Task.Delay(1000); // time to clean up
            _cts.Dispose();
        }

        private async Task ListenForServiceConnectionAsync(CancellationToken ct)
        {
            Console.WriteLine($"nprs:Listening for {this.ServicePipeName} connection.");
            await _servicePipe.WaitForConnectionAsync(ct);
            Console.WriteLine($"nprs:{this.ServicePipeName} connected.");
        }

        private async Task ProcessProxyRequestAsync(CancellationToken ct)
        {
            try
            {
                if (_proxyPipe != null && !_proxyPipe.IsConnected)
                {
                    if (ct.IsCancellationRequested) return;
                    Console.WriteLine($"nprs:Listening for {this.ProxyPipeName} connection.");
                    await _proxyPipe.WaitForConnectionAsync(ct);
                    Console.WriteLine($"nprs:{this.ProxyPipeName} connected.");
                }

                if (ct.IsCancellationRequested) return;
                //Console.WriteLine($"nprs:Waiting to receive {_proxyPipeName} request.");
                var request = Serializer.DeserializeWithLengthPrefix<TRequest>(_proxyPipe, PrefixStyle.Base128);
                Console.WriteLine($"nprs:Received {this.ProxyPipeName} request: {request}.");

                TResponse response = null;
                if (_servicePipe != null && _servicePipe.IsConnected)
                {
                    if (ct.IsCancellationRequested) return;
                    Console.WriteLine($"nprs:Forwarding {this.ServicePipeName} request: {request}.");
                    Serializer.SerializeWithLengthPrefix(_servicePipe, request, PrefixStyle.Base128);
                    //Console.WriteLine($"nprs:Forwared {_servicePipeName} request.");

                    if (ct.IsCancellationRequested) return;
                    //Console.WriteLine($"nprs:Waiting to receive {_servicePipeName} response.");
                    response = Serializer.DeserializeWithLengthPrefix<TResponse>(_servicePipe, PrefixStyle.Base128);
                    Console.WriteLine($"nprs:Received {this.ServicePipeName} response: {response}.");
                }


                if (ct.IsCancellationRequested) return;
                //Console.WriteLine($"nprs:Returning {_proxyPipeName} response.");
                Serializer.SerializeWithLengthPrefix(_proxyPipe, response, PrefixStyle.Base128);
                Console.WriteLine($"nprs:Returned {this.ProxyPipeName} response.");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_proxyPipe != null && _proxyPipe.IsConnected)
                    _proxyPipe.Disconnect();
            }
        }
    }
}
