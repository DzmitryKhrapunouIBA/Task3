using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private const int NumberOfQuestions = 4;
        private ITestService TestService { get; }
        private IUserService UserService { get; }
        private IQuestionService QuestionService { get; }
        private IAnswerService AnswerService { get; }

        public UserController(ITestService testService, IUserService userService, IQuestionService questionService, IAnswerService answerService)
        {
            TestService = testService;
            UserService = userService;
            QuestionService = questionService;
            AnswerService = answerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("allUsers")]
        public async Task<IActionResult> Get(CancellationToken token = default)
        {
            var users = await UserService.AllNamedAsync(null, token);
            if (users.Count() == 0)
                return NotFound(ResultBase.Failure(System.Net.HttpStatusCode.NotFound));

            return Ok(Result<NamedEntity>.Ok(users));
        }
    }
}