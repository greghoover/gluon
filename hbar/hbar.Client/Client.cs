using hbar.Contract;
using hbar.Contract.FileSystemQuery;
using hbar.Proxy.FileSystemQuery;
using System;
using System.Collections.Generic;

namespace hbar.Client
{
    public class Client
    {
        public Client() { }

        public static IEnumerable<(int Id, string Desc)> GetServices()
        {
            var serviceTypesEnum = typeof(ServiceTypesEnum);
            foreach (var vlu in Enum.GetValues(serviceTypesEnum))
            {
                var id = Convert.ToInt32(vlu);
                var desc = Enum.GetName(serviceTypesEnum, id);
                yield return (id, desc);
            }
        }

        public string DoFileSystemQuery(string folderPath)
        {
            var proxy = new FileSystemQueryProxy();
            var request = new FileSystemQueryRequest(FileSystemQueryTypeEnum.DirectoryExists, folderPath);

            var response = proxy.Execute(request);

            return response.ResponseString;
        }

    }
}
