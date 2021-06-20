using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    //[Route("api/registration")]
    public class RegistrationController : Controller
    {
        private IUserService UserService { get; }

        public RegistrationController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet("registration")]
        public IActionResult Registration()
        {
            return View("Registration");
        }

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

                return Json(ResultBase.Ok(System.Net.HttpStatusCode.OK));
            }
            catch (System.Exception e)
            {
                return Json(ResultBase.Failure(System.Net.HttpStatusCode.BadRequest, e?.Message));
            }
        }
    }
}