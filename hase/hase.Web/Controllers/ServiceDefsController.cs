using hase.DevLib.Framework.Repository.Contract;
using hase.DevLib.Framework.Repository.Service;
using Microsoft.AspNetCore.Mvc;

namespace hase.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceDefsController : ControllerBase
	{
		// GET api/servicedefs
		[HttpGet]
		public IActionResult Get()
		{
			return new ObjectResult(ServiceRepo.GetAllFolders());
		}

		// GET api/servicedefs/5
		[HttpGet("{serviceName}")]
		public IActionResult Get(string serviceName)
		{
			var serviceFolder = ServiceRepo.GetFolder(serviceName);
			return new ObjectResult(serviceFolder);
		}

		// POST api/servicedefs
		[HttpPost]
		public IActionResult CreateDocument([FromBody] FolderSpec serviceFolder)
		{
			ServiceRepo.SaveFolder(serviceFolder);
			return new ObjectResult(serviceFolder);
		}

		// DELETE api/servicedefs/5
		[HttpDelete("{serviceName}")]
		public IActionResult Delete(string serviceName)
		{
			ServiceRepo.DeleteFolder(serviceName);
			return new OkResult();
		}
	}
}
