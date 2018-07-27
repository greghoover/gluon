using System.Collections.Generic;

namespace hase.DevLib.Framework.Repository.Contract
{
	public class FolderSpec
	{
		public string FolderName { get; set; }
		public IEnumerable<FileSpec> Files { get; set; }
	}
}
