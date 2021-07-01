using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IBA.Task3.Controllers
{
    //[Authorize]
    public class TestEditorController : BaseController
    {
        private const int NumberOfQuestions = 4;
        private ITestService TestService { get; }
        private IUserService UserService { get; }
        private IQuestionService QuestionService { get; }
        private IAnswerService AnswerService { get; }

        public TestEditorController(ITestService testService, IUserService userService, IQuestionService questionService, IAnswerService answerService)
        {
            TestService = testService;
            UserService = userService;
            QuestionService = questionService;
            AnswerService = answerService;
        }

        [HttpPost("/getTestEditorLink")]
        public IActionResult GetTestEditorLink()
        {
            string path = "testEditorPage.html";
            return Json(path);
        }
    }
}
