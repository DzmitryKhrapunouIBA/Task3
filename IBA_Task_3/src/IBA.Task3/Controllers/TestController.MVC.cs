using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    //[Authorize]
    public partial class TestController : BaseController
    {
        [HttpGet("testCreator")]
        public IActionResult Create()
        {
            return View("TestCreator");
        }

        [HttpGet("testEditor")]
        public IActionResult Edit()
        {
            return View("TestEditor");
        }
    }
}