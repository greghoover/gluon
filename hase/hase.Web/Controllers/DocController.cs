using hase.DevLib.Framework.Utility;
using Microsoft.AspNetCore.Mvc;
using System;

namespace hase.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var model = new CreateDocumentModel()
			{
				Document = new byte[] { 0x03, 0x10, 0xFF, 0xFF },
				Name = "Test",
				CreationDate = new DateTime(2017, 12, 27)
			};

			return new ObjectResult(model);
		}
		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			var model = new CreateDocumentModel()
			{
				Document = new byte[] { 0x03, 0x10, 0xFF, 0xFF },
				Name = "Test",
				CreationDate = new DateTime(2017, 12, 27)
			};

			return new ObjectResult(model);
		}
		[HttpPost]
		public IActionResult CreateDocument([FromBody] CreateDocumentModel model)
		{
			return new ObjectResult(model);
		}
	}
}
