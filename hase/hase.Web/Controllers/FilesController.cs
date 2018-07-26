using hase.DevLib.Framework.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace hase.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		// GET api/files
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return FileRepo.GetAllFileNames();
		}

		// GET api/files/fred.dll
		[HttpGet("{fileName}")]
		public ActionResult<byte[]> Get(string fileName)
		{
			return FileRepo.GetFile(fileName);
		}

		// POST api/files
		//[HttpPost]
		//public void Post([FromBody] byte[] fileBytes)
		//{
		//}

		// PUT api/files/fred.dll
		[HttpPut("{id}")]
		public void Put(string fileName, [FromBody] byte[] fileBytes)
		{
			FileRepo.SaveFile(fileName, fileBytes);
		}

		// DELETE api/files/fred.dll
		[HttpDelete("{id}")]
		public void Delete(string fileName)
		{
			FileRepo.DeleteFile(fileName);
		}
	}
}
