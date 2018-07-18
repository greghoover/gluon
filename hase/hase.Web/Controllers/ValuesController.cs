using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace hase.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		public string DefaultTenant => "Tenant0";

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(string id)
		{
			return System.IO.File.ReadAllText(Path.Combine(DefaultTenant, id + ".json"));
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
			System.IO.Directory.CreateDirectory(DefaultTenant);
			System.IO.File.WriteAllText(Path.Combine(DefaultTenant, id + ".json"), value.ToString());
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(string id)
		{
		}
	}
}
