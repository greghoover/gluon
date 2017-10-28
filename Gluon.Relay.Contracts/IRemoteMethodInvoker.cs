using System;
using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface IRemoteMethodInvoker : IDisposable
    {
        Task InvokeAsync(string methodName, params object[] args);

        Task<TResult> InvokeAsync<TResult>(string methodName, params object[] args);
    }
}
