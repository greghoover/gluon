using Gluon.Relay.Contracts;

namespace Gluon.Tester.Contracts
{
    public class FileSystemQueryRqst : RelayMessageBase
    {
        public FileSystemQueryTypeEnum QueryType { get; private set; }
        public string FolderPath { get; private set; }

        public FileSystemQueryRqst(FileSystemQueryTypeEnum queryType, string folderPath)
        {
            QueryType = queryType;
            FolderPath = folderPath;
        }

        public override string ToString() =>
             $"QueryType[{this.QueryType}] Path[{this.FolderPath}].";
    }
}
