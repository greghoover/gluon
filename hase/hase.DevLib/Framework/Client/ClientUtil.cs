using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace hase.DevLib.Framework.Client
{
	public static class ClientUtil
	{
		public enum ServiceLocation
		{
			Local = 0,
			Remote = 1,
		}

		public static IList<string> GetReadableEnumNames<EnumType>()
			where EnumType : struct, IConvertible, IComparable, IFormattable // i.e. an enum
		{
			return Enum.GetNames(typeof(EnumType)).Select(b => b.SplitCamelCase()).ToList();
		}

		public static string SplitCamelCase(this string str)
		{
			return Regex.Replace(
				Regex.Replace(
					str,
					@"(\P{Ll})(\P{Ll}\p{Ll})",
					"$1 $2"
				),
				@"(\p{Ll})(\P{Ll})",
				"$1 $2"
			);
		}

		// Experimental
		//public static T WithLocal<T>(this T client)
		//    where T : class, IServiceClient<IService<AppRequestMessage, AppResponseMessage>, AppRequestMessage, AppResponseMessage>
		//{
		//    //var local = Service.Service<>
		//    //var local = Activator.CreateInstance<TService, IService<AppRequestMessage, AppResponseMessage>>();
		//    //client.Service = default(IService<AppRequestMessage, AppResponseMessage>);
		//    return client as T;
		//}
	}
}
