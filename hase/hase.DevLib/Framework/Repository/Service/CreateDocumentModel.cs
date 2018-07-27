using System;

namespace hase.DevLib.Framework.Repository.Service
{
	public class CreateDocumentModel
	{
		public byte[] Document { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
	}
}