using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IBA.Task3.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected string Login => User.Identity.Name;

        protected int GetUserId()
        {
            return int.Parse(HttpContext.User.Claims.First(i => i.Type == "UserId").Value);
        }
    }
}