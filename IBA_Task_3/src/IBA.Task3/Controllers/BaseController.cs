using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string Login => User.Identity.Name;

        protected int GetUserId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }
    }
}