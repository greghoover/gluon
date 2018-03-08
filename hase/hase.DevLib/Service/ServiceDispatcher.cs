using hase.DevLib.Contract.FileSystemQuery;
using hase.DevLib.Service.FileSystemQuery;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hase.DevLib.Service
{
    public class ServiceDispatcher
    {
        private static readonly string pipeName = nameof(FileSystemQueryService);
        private NamedPipeClientStream pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None);

        public void Run()
        {
            Console.WriteLine($"nprc:{pipeName} connecting to relay.");
            pipe.ConnectAsync(5000).Wait();
            Console.WriteLine($"nprc:{pipeName} connected to relay.");

            while (true)
            {
                ProcessRequest();
            }

        }

        private void ProcessRequest()
        {
            //Console.WriteLine($"nprc:Waiting to receive {pipeName} request.");
            var request = Serializer.DeserializeWithLengthPrefix<FileSystemQueryRequest>(pipe, PrefixStyle.Base128);
            Console.WriteLine($"nprc:Received {pipeName} request: {request}.");

            var service = new FileSystemQueryService();
            FileSystemQueryResponse response = null;

            try
            {
                response = service.Execute(request);
            }
            catch (Exception ex) { }

            Console.WriteLine($"nprc:Sending {pipeName} response: {response}.");
            Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
            //Console.WriteLine($"nprc:Sent {pipeName} response.");
        }
    }
}

