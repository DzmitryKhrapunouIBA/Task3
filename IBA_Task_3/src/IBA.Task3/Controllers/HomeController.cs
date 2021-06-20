using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{

    public class HomeController : Controller
    {

        public HomeController()
        {
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Home")]
        public IActionResult Home()
        {
            return View("Home");
        }


        //[Authorize]
        //[HttpGet("Home")]
        //public IActionResult Home()
        //{
        //    var response = new
        //    {
        //        url = "Home"
        //    };

        //    return Json(response);
        //}
    }
}
