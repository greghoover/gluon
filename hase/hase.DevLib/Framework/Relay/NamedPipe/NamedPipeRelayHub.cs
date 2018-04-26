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

            //var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            //var sid = WindowsIdentity.GetCurrent().User;
            //var r0 = new PipeAccessRule(sid, PipeAccessRights.FullControl, AccessControlType.Allow);
            
            //var r1 = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, AccessControlType.Allow);
            //var r2 = new PipeAccessRule("Users", PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance, AccessControlType.Allow);
            //var r3 = new PipeAccessRule("CREATOR OWNER", PipeAccessRights.FullControl, AccessControlType.Allow);
            //var r4 = new PipeAccessRule("SYSTEM", PipeAccessRights.FullControl, AccessControlType.Allow);

            _proxyPipe = new NamedPipeServerStream(this.ProxyPipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            //var proxyAccess = _proxyPipe.GetAccessControl();
            //proxyAccess.AddAccessRule(r0);
           // _proxyPipe.SetAccessControl(proxyAccess);

            _servicePipe = new NamedPipeServerStream(this.ServicePipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            //var serviceAccess = _proxyPipe.GetAccessControl();
            //serviceAccess.AddAccessRule(r0);
            //_servicePipe.SetAccessControl(serviceAccess);
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
                var e = ex; // no compiler warning please
            }
            finally
            {
                if (_proxyPipe != null && _proxyPipe.IsConnected)
                    _proxyPipe.Disconnect();
            }
        }
    }
}
