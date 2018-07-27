namespace hase.DevLib.Framework.Repository.Contract
{
	public class FileSpec
	{
		public string FileName { get; set; }
		public string[] RelativeSubFolder { get; set; }
		public byte[] Content { get; set; }
	}
}