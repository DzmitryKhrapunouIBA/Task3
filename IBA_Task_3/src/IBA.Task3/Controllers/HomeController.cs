using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //[AllowAnonymous]
        [HttpGet("Home")]
        public IActionResult Home()
        {
            //return RedirectToPage("Home");
            return View("Home");
        }


        //[Route("Home")]
        //public IActionResult Home()
        //{
        //    var response = new
        //    {
        //        view = View("Home")
        //    };

        //    return Json(response);
        //}
    }
}
