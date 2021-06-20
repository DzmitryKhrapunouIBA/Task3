using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Servises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    public class LoginController : Controller
    {
        private IUserService UserService { get; }

        private IJwtGenerator JwtGenerator { get; }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public LoginController(IUserService userService, IJwtGenerator jwtGenerator)
        {
            UserService = userService;
            JwtGenerator = jwtGenerator;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        /// <summary>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<IActionResult> Token(AuthModel model = default, CancellationToken token = default)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest(ResultBase.BadRequest(ModelState));

            try
            {
                var user = await UserService.GetAsync(
                    u => u.Login == model.Login && u.Password == AuthService.GetHash(model.Password), token);

                if (user == null)
                    return BadRequest(ResultBase.BadRequest("Неверный логин или пароль"));

                var access_token = JwtGenerator.CreateToken(user);

                var response = new
                {
                    access_token,
                    username = user.Login
                };

                return Json(response);
            }
            catch (System.Exception e)
            {
                return Json(ResultBase.Failure(e?.Message));
            }
        }

        //[HttpPost("Ok")]
        //public IActionResult Ok()
        //{
        //    return RedirectToAction("Home", "Home");
        //}

        [HttpPost("Ok")]
        public IActionResult Ok()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }
            return Content("не аутентифицирован");
        }
    }
}