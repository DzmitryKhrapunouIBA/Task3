using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Authorize]
    public class AuthenticationController : Controller
    {
        private IUserService UserService { get; }

        private IJwtGenerator JwtGenerator { get; }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public AuthenticationController(IUserService userService, IJwtGenerator jwtGenerator)
        {
            UserService = userService;
            JwtGenerator = jwtGenerator;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpGet("registration")]
        public IActionResult Registration()
        {
            return View("Registration");
        }

        /// <summary>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
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
            catch (Exception e)
            {
                return Json(ResultBase.Failure(e?.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegistrationModel model, CancellationToken token = default)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                    return BadRequest(ResultBase.BadRequest(ModelState));

                var user = await UserService.GetAsync(u => u.Login == model.Login, token);

                if (user != null)
                    return BadRequest(ResultBase.BadRequest("Пользователь с таким Логином уже зарегестрирован"));

                var entry = await UserService.CreateAsync(new User()
                {
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    Login = model.Login,
                    SurName = model.Surname,
                    Password = AuthService.GetHash(model.Password),
                }, token);

                return Json(ResultBase.Ok(HttpStatusCode.OK));
            }
            catch (Exception e)
            {
                return Json(ResultBase.Failure(HttpStatusCode.BadRequest, e?.Message));
            }
        }
    }
}