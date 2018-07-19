using hase.Web.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace hase.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<JObject>> Get()
		{
			return FormDefRepo.GetAllFormDefinitions();
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<JObject> Get(string id)
		{
			return FormDefRepo.GetFormDefinition(id);
		}

		// POST api/values
		//[HttpPost]
		//public void Post([FromBody] JObject value)
		//{
		//}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(string id, [FromBody] JObject value)
		{
			FormDefRepo.SaveFormDefinition(id, value);
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(string id)
		{
			FormDefRepo.DeleteFormDefinition(id);
		}
	}
}
