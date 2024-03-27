using Microsoft.AspNetCore.Mvc;

namespace testApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {

        private readonly IAppRepository _appRepository;
        public AppController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<App>))]
        public IActionResult GetApps()
        {
            var apps = _appRepository.GetApps();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(apps);
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(200, Type = typeof(App))]
        [ProducesResponseType(404)]
        public IActionResult GetApp(int ID)
        {

            var isExist = _appRepository.AppExists(ID);
            if (!isExist)
            {
                return NotFound();
            }

            var app = _appRepository.GetApp(ID);
            JsonAppender jsonAppender = new JsonAppender(ID.ToString());
            string jsonData = jsonAppender.getFileString();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { app, jsonData });
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult CreateApp([FromBody] CreateApp AppCreate)
        {
            var createdId = _appRepository.CreateApp(AppCreate.Nickname, DateTime.Now);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createdId == 0)
            {
                return NotFound();
            }

            // add a json file for future use
            JsonAppender jsonAppender = new JsonAppender(createdId.ToString());
            jsonAppender.CreateEmptyFile();

            return Ok(createdId > 0);
        }

        [HttpPut("{ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult AddTranslationKey([FromBody] TranlationKeyValue tranlationKeyValue, [FromRoute] int ID)
        {
            var isExist = _appRepository.AppExists(ID);
            if (!isExist)
            {
                return NotFound();
            }

            // TODO set the json file that is saved on the server
            var isServerUpdated = true;
            JsonAppender jsonAppender = new JsonAppender(ID.ToString());
            jsonAppender.AppendJson(tranlationKeyValue);
            // update the "last updated" for this appj
            var isDbUpdated = _appRepository.UpdateLastUpdated(ID, DateTime.Now);

            string errorMsg = !isServerUpdated ? "Error while updating the server; " : "";
            if (!isDbUpdated) errorMsg += "Error while updating the DB; ";
            if (errorMsg.Length > 0)
            {
                ModelState.AddModelError("", errorMsg);
                return StatusCode(500, ModelState);
            }

            return Ok("Translation file was successfully updated.");
        }
    }

}
