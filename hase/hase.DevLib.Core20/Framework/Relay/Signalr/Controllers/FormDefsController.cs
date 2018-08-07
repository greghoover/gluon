using hase.DevLib.Framework.Repository.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace hase.Relays.Signalr.Server.Controllers
{
    [Route("api/values")] // backward compat for now
	[Route("api/[controller]")]
	[ApiController]
	public class FormDefsController : ControllerBase
	{
		// GET api/formdefs
		[HttpGet]
		public ActionResult<IEnumerable<JObject>> Get()
		{
			return FormDefRepo.GetAllFormDefinitions();
		}

		// GET api/formdefs/5
		[HttpGet("{id}")]
		public ActionResult<JObject> Get(string id)
		{
			return FormDefRepo.GetFormDefinition(id);
		}

		// POST api/formdefs
		//[HttpPost]
		//public void Post([FromBody] JObject value)
		//{
		//}

		// PUT api/formdefs/5
		[HttpPut("{id}")]
		public void Put(string id, [FromBody] JObject value)
		{
			FormDefRepo.SaveFormDefinition(id, value);
		}

		// DELETE api/formdefs/5
		[HttpDelete("{id}")]
		public void Delete(string id)
		{
			FormDefRepo.DeleteFormDefinition(id);
		}
	}
}
