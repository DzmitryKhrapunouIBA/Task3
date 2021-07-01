using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises;
using IBA.Task3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBA.Task3.Controllers
{
    //[Authorize]
    //[Route("api/test")]
    public partial class TestController : BaseController
    {
        private ITestService TestService { get; }
        private IUserService UserService { get; }
        private IQuestionService QuestionService { get; }
        private IAnswerService AnswerService { get; }
        private ITestAssignmentService TestAssignmentService { get; }

        private ITestResultService TestResultService { get; }

        public TestController(ITestService testService, IUserService userService, IQuestionService questionService, 
                              IAnswerService answerService, ITestAssignmentService testAssignmentService,
                              ITestResultService testResultService)
        {
            TestService = testService;
            UserService = userService;
            QuestionService = questionService;
            AnswerService = answerService;
            TestAssignmentService = testAssignmentService;
            TestResultService = testResultService;
        }

        [Route("MyTests")]
        public ActionResult GetMyTests()
        {
            return View("MyTests");
        }

        [Route("NewTest")]
        public ActionResult GetNewTest()
        {
            return View("TestCreator");
        }

        [Route("NewQuestionWithAnswers")]
        public ActionResult GetNewQuestionWithAnswers()
        {
            return View("TestEditor");
        }

        [HttpGet("User")]
        public async Task<IActionResult> Get(CancellationToken token = default)
            => await Get(GetUserId(), token);

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> Get(int userId, CancellationToken token = default)
        {
            try
            {
                var test = await TestService.GetAsync(t => t.UserId == userId, token);
                if (test == null)
                    return NotFound(ResultBase.NotFound());

                return Ok(ResultModel<Test>.Ok(test));
            }
            catch (System.Exception e)
            { 
                return Json(ResultBase.Failure(e?.Message));
            }
        }

        /// <summary>
        /// Получаем все назначенные тесты не пользователя.
        /// Если код пользователя не указан - получаем все назначенные тесты текущего пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя, кому назначены тесты.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Возвращает список назначенных тестов.</returns>
        [HttpGet("assignment/{userId?}")]
        public async Task<IActionResult> GetAllAssignedTests(int? userId, CancellationToken token = default)
        {
            //DbTestData dbTest = new DbTestData();

            if (!userId.HasValue)
                userId = GetUserId();

            var assignments = await TestAssignmentService.AllAsync(a => a.UserId == userId, token, x=> x.Test);

            if (!assignments.Any())
                return NotFound(ResultBase.Failure(System.Net.HttpStatusCode.NotFound));

            return Ok(Result<Test>.Ok(assignments.Select(x => x.Test)));
        }

        /// <summary>
        /// Получаем все результаты тестов не пользователя.
        /// Если код пользователя не указан - получаем все результаты тестов текущего пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Возвращает список результатов тестов.</returns>
        [HttpGet("result/{userId?}")]
        public async Task<IActionResult> Get(int? userId, CancellationToken token = default)
        {
            if (!userId.HasValue)
                userId = GetUserId();

            var assignments = await TestResultService.AllAsync(a => a.UserId == userId, token, x => x.Test);

            if (!assignments.Any())
                return NotFound(ResultBase.Failure(System.Net.HttpStatusCode.NotFound));

            return Ok(Result<Test>.Ok(assignments.Select(x => x.Test)));
        }

        //------------------------------------
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TestModel model, CancellationToken token = default)
        {
            var tests = await TestService.AllAsync(t => t.Name == model.Name && t.UserId == GetUserId(), token);

            //if (tests.Any())
            //    return BadRequest(ResultBase.BadRequest("Тест с таким именем уже существует"));

            var entry = await TestService.CreateAsync(
                        new Test(model.Name, GetUserId(), model.Attempts), token);

            return Ok(ResultModel<TestCreateModel>.Ok(
                      new TestCreateModel() { Name = entry.Name, Attempts = entry.Attempts, UserName = "Dima" /*Login*/ }));
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(TestUpdateModel model, CancellationToken token = default)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest(ResultBase.BadRequest(ModelState));

            var test = await TestService.GetAsync(t => t.Name == model.Name && t.UserId == GetUserId(), token);

            var questions = await QuestionService.AllAsync(t => t.TestId == test.Id);

            var question = await QuestionService.CreateAsync(
                    new Question() { TestId = test.Id, Name = model.Question }
                );

            int idx = 1;
            foreach (var answer in new string[] { model.Answer1, model.Answer2, model.Answer3, model.Answer4 })
            {
                await AnswerService.CreateAsync(
                        new Answer() { Name = answer, QuestionId = question.Id, CorrectAnswer = model.CorrectAnswer == idx },
                        token
                    );
            }

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(TestUpdateModel model, CancellationToken token = default)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest(ResultBase.BadRequest(ModelState));

            var test = await TestService.GetAsync(t => t.Name == model.Name && t.UserId == GetUserId(), token);

            var questions = await QuestionService.AllAsync(t => t.TestId == test.Id);

            var question = await QuestionService.CreateAsync(
                    new Question() { TestId = test.Id, Name = model.Question }
                );

            int idx = 1;
            foreach (var answer in new string[] { model.Answer1, model.Answer2, model.Answer3, model.Answer4 })
            {
                await AnswerService.CreateAsync(
                        new Answer() { Name = answer, QuestionId = question.Id, CorrectAnswer = model.CorrectAnswer == idx },
                        token
                    );
            }

            return Ok();
        }
    }
}