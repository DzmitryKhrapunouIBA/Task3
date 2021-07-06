using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    //    [Authorize]
    public class UserController : Controller
    {
        private IUserService UserService { get; }
       
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet("allUsers")]
        public async Task<IActionResult> Get(CancellationToken token = default)
        {
            var users = await UserService.AllNamedAsync(null, token);
            if (users.Count() == 0)
                return NotFound(ResultBase.Failure(HttpStatusCode.NotFound));

            return Ok(Result<NamedEntity>.Ok(users));
        }
    }
}