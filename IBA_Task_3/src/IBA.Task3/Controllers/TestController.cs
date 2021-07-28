using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;
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

        /// <summary>
        /// Получаем все тесты пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Возвращает список тестов.</returns>

        [AllowAnonymous]
        [HttpGet("MyTests")]
        public async Task<IActionResult> GetMyTests(CancellationToken token = default)
        {
            try
            {
                //var user = await UserService.GetAsync(GetUserId(), token);
                //var test = await TestService.GetAsync(t => t.UserId == GetUserId(), token);
                var tests = await TestService.AllAsync(t => t.UserId == 2, token, x => x.Questions);

                if (tests == null)
                    return NotFound(ResultBase.Failure(HttpStatusCode.NotFound));

                return View("MyTests", tests);
            }
            catch (Exception e)
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

        [AllowAnonymous]
        [HttpGet("Assignment/{userId?}")]
        public async Task<IActionResult> GetAllAssignedTests(int? userId, CancellationToken token = default)
        {
            try
            {
                if (!userId.HasValue)
                    userId = /*GetUserId()*/ 2;

                var assignments = await TestAssignmentService.AllAsync(a => a.UserId == userId, token, x => x.Test);

                if (!assignments.Any())
                    return NotFound(ResultBase.Failure(HttpStatusCode.NotFound));

                return View("MyAssignmentTests", assignments);
            }
            catch (Exception e)
            {
                return Json(ResultBase.Failure(e?.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("NewTest")]
        public ActionResult NewTest()
        {
            return View("TestCreator");
        }

        [AllowAnonymous]
        [HttpGet("NewQuestionWithAnswers")]
        public ActionResult GetNewQuestionWithAnswers()
        {
            return View("TestEditor");
        }

        [HttpGet("User")]
        public async Task<IActionResult> Get(CancellationToken token = default)
            => await Get(GetUserId(), token);

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> Get(int userId, CancellationToken token = default)
        //{
        //    try
        //    {
        //        var test = await TestService.GetAsync(t => t.UserId == userId, token);
        //        if (test == null)
        //            return NotFound(ResultBase.NotFound());

        //        return Ok(ResultModel<Test>.Ok(test));
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(ResultBase.Failure(e?.Message));
        //    }
        //}

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
                return NotFound(ResultBase.Failure(HttpStatusCode.NotFound));

            return Ok(Result<Test>.Ok(assignments.Select(x => x.Test)));
        }

        //------------------------------------
        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TestModel model, CancellationToken token = default)
        {
            //var user = await UserService.GetAsync(GetUserId(), token);
            var tests = await TestService.AllAsync(t => t.UserId == GetUserId() && t.Name == model.Name, token);

            if (tests.Any())
                return BadRequest(ResultBase.BadRequest("Тест с таким именем уже существует"));

            var entry = await TestService.CreateAsync(
                        new Test(model.Name, GetUserId(), model.Attempts), token);

            return Ok(ResultModel<TestCreateModel>.Ok(
                      new TestCreateModel() { Name = entry.Name, Attempts = entry.Attempts, UserName = Login }));
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(TestUpdateModel model, CancellationToken token = default)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest(ResultBase.BadRequest(ModelState));

            var test = await TestService.GetAsync(t => t.Name == model.Name && t.UserId == GetUserId(), token);

            var questions = await QuestionService.AllAsync(t => t.TestId == test.Id);

            var question = await QuestionService.CreateAsync(new Question() { TestId = test.Id, Name = model.Question });

            for (int i = 0; i < model.Answers.Count(); i++)
            {
                await AnswerService.CreateAsync(
                        new Answer() { Name = model.Answers[i], QuestionId = question.Id, CorrectAnswer = model.CorrectAnswer == i + 1 },
                        token
                     );
            }

            return Ok();
        }

        [HttpPost("Update/{userId?}")]
        public async Task<IActionResult> Update(TestUpdateModel model, CancellationToken token = default)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest(ResultBase.BadRequest(ModelState));

            var test = await TestService.GetAsync(t => t.Name == model.Name && t.UserId == GetUserId(), token);

            var questions = await QuestionService.AllAsync(t => t.TestId == test.Id);

            var question = await QuestionService.CreateAsync(
                    new Question() { TestId = test.Id, Name = model.Question }
                );

            for (int i = 0; i < model.Answers.Count(); i++)
            {
                await AnswerService.CreateAsync(
                        new Answer() { Name = model.Answers[i], QuestionId = question.Id, CorrectAnswer = model.CorrectAnswer == i + 1 },
                        token
                     );
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Delete/{userId?}")]
        public async Task<IActionResult> Delete(int? id, CancellationToken token = default)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await TestService.GetAsync(t => t.Id == id, token);
            
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        [AllowAnonymous]
        [HttpPost("Delete")]
        public async Task<bool> DeleteConfirmed(int id, CancellationToken token = default)
        {
            return await TestService.DeleteAsync(id, token);
        }

    }
}