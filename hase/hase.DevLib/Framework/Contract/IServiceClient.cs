using System;

namespace hase.DevLib.Framework.Contract
{
	//public interface IServiceClient
	//{
	//	IService<AppRequestMessage, AppResponseMessage> Service { get; }
	//	string RequestClrType { get; }
	//	string ResponseClrType { get; }
	//	string ServiceClrType { get; }
	//}

	public interface IServiceClient<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		IService<TRequest, TResponse> Service { get; }
		string RequestClrType { get; }
		string ResponseClrType { get; }
		string ServiceClrType { get; }
	}
}
