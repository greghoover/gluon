using hbar.Contract.FileSystemQuery;
using ProtoBuf;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace hbar.Proxy.FileSystemQuery
{
    public class FileSystemQueryProxy : IFileSystemQueryService
    {
        public FileSystemQueryResponse Execute(FileSystemQueryRequest request)
        {
            FileSystemQueryResponse response = null;
            var pipeName = nameof(FileSystemQueryProxy);

            var pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None);
            Console.WriteLine($"{pipeName} connecting to relay.");
            pipe.ConnectAsync(5000).Wait();
            Console.WriteLine($"{pipeName} connected.");

            Console.WriteLine($"Sending {pipeName} request: {request}.");
            Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);
            //Console.WriteLine($"Sent {pipeName} request.");

            //Console.WriteLine($"Receiving {pipeName} response.");
            response = Serializer.DeserializeWithLengthPrefix<FileSystemQueryResponse>(pipe, PrefixStyle.Base128);
            Console.WriteLine($"Received {pipeName} response: {response}.");

            return response;
        }
    }
}
