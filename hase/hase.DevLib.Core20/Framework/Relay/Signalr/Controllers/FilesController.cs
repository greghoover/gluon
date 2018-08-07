using hase.DevLib.Framework.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace hase.Relays.Signalr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        // GET: api/files
        [HttpGet]
        public ActionResult Get()
        {
            return new ObjectResult(FileRepo.GetAllFiles());
        }

        // GET: api/files/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return new FileContentResult(null, "");
        }

        // POST api/files
        [HttpPost]
        public IActionResult Post([FromBody] FileSpec file)
        {
            FileRepo.SaveFile(file);
            return new FileContentResult(null, "");
        }
    }
}
