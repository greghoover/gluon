using hase.DevLib.Framework.Repository.Service;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Utility
{
	public static class Util
	{
		public static async Task<TBase> GeneralizeTask<TBase, TDerived>(Task<TDerived> task)
			where TDerived : TBase
		{
			return (TBase)await task;
		}
	}
}
