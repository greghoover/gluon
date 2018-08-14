using Microsoft.Extensions.Primitives;
using System;

namespace hase.DevLib.Framework.Utility
{
    public class AltChangeToken : IChangeToken
    {
        public bool HasChanged => false;
        public bool ActiveChangeCallbacks => false;
        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => default;
    }
}
