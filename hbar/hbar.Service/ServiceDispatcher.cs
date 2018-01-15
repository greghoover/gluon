using hbar.Contract.FileSystemQuery;
using hbar.Service.FileSystemQuery;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hbar.Service
{
    public class ServiceDispatcher
    {
        private static readonly string pipeName = nameof(FileSystemQueryService);
        private NamedPipeClientStream pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None);

        public void Run()
        {
            Console.WriteLine($"{pipeName} connecting to relay.");
            pipe.ConnectAsync(5000).Wait();
            Console.WriteLine($"{pipeName} connected to relay.");

            while (true)
            {
                ProcessRequest();
            }

        }

        private void ProcessRequest()
        {
            //Console.WriteLine($"Waiting to receive {pipeName} request.");
            var request = Serializer.DeserializeWithLengthPrefix<FileSystemQueryRequest>(pipe, PrefixStyle.Base128);
            Console.WriteLine($"Received {pipeName} request: {request}.");

            var service = new FileSystemQueryService();
            FileSystemQueryResponse response = null;

            try
            {
                response = service.Execute(request);
            }
            catch (Exception ex) { }

            Console.WriteLine($"Sending {pipeName} response: {response}.");
            Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
            //Console.WriteLine($"Sent {pipeName} response.");
        }
    }
}
