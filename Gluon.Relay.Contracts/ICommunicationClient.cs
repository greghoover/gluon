using System;
using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface ICommunicationClient : IInvoker, IDisposable { }

    public interface IInvoker
    {
        Task InvokeAsync(string methodName, params object[] args);
        Task<TResult> InvokeAsync<TResult>(string methodName, params object[] args);
    }
}
