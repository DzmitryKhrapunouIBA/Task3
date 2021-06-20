using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    [Authorize]
    public class AchievementsController : Controller
    {

        public AchievementsController()
        {
        }

        //[Authorize]
        //[HttpGet("/myAchievements")]
        //public async Task<ActionResult<Test>> Get(int id)
        //{
        //    Test test = await db.Tests.FirstOrDefaultAsync(x => x.Id == id);
        //    if (test == null)
        //        return NotFound();
        //    return new ObjectResult(test);
        //}



        //[Authorize]
        //[HttpGet("/myAchievements")]
        //public IActionResult Achievements(string login)
        //{
        //    EFUnitOfWork unitOfWork = new EFUnitOfWork(db);

        //    var users = unitOfWork.Users.Find(c => c.Login == login);

        //    var newTest = new Test(testName, users.First(), attempts, numberOfQuestions);

        //    unitOfWork.Tests.Create(newTest);

        //    var response = new { TestName = testName };

        //    return Json(response);
        //}
    }
}
