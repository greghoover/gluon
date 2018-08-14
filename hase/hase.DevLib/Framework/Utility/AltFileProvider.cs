using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace hase.DevLib.Framework.Utility
{
    public class AltFileProvider : IFileProvider
    {
        public IFileInfo GetFileInfo(string subpath)
        {
            return new AltFileInfo(subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AltDirectoryContents();
        }

        public IChangeToken Watch(string filter)
        {
            return new AltChangeToken();
        }

    }
}
