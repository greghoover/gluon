using hbar.Contract.FileSystemQuery;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hbar.Proxy.FileSystemQuery
{
    public class FileSystemQueryProxy : IFileSystemQueryService
    {
        public FileSystemQueryResponse Execute(FileSystemQueryRequest request)
        {
            FileSystemQueryResponse response = null;
            var pipeName = nameof(FileSystemQueryProxy);

            var pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None);
            Console.WriteLine($"nprc:{pipeName} connecting to relay.");
            pipe.ConnectAsync(5000).Wait();
            Console.WriteLine($"nprc:{pipeName} connected.");

            Console.WriteLine($"nprc:Sending {pipeName} request: {request}.");
            Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);
            //Console.WriteLine($"nprc:Sent {pipeName} request.");

            //Console.WriteLine($"nprc:Receiving {pipeName} response.");
            response = Serializer.DeserializeWithLengthPrefix<FileSystemQueryResponse>(pipe, PrefixStyle.Base128);
            Console.WriteLine($"nprc:Received {pipeName} response: {response}.");

            return response;
        }
    }
}
