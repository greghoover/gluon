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
			//LocalService = 0,
			Local = 1,
			Remote = 2
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
	}
}
