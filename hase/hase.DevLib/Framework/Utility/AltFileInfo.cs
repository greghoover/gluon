using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Text;

namespace hase.DevLib.Framework.Utility
{
    public class AltFileInfo : IFileInfo
    {
        public long Length => CreateReadStream().Length;
        public string PhysicalPath { get; }
        public string Name { get; }
        public DateTimeOffset LastModified => DateTimeOffset.Now;
        public bool IsDirectory => false;

        public AltFileInfo(string path)
        {
            PhysicalPath = path;
            Name = Path.GetFileName(path);
        }

        public Stream CreateReadStream()
        {
            var txt = File.ReadAllText(PhysicalPath);
            return new MemoryStream(Encoding.UTF8.GetBytes(txt));
        }

        public bool Exists => File.Exists(this.PhysicalPath);
    }
}
