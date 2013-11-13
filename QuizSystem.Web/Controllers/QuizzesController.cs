using QuizSystem.Model;
using QuizSystem.Web.Extensions.FiltersExtensions;
using QuizSystem.Web.Libs.DataPager;
using QuizSystem.Web.Libs.Helpers;
using QuizSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;

namespace QuizSystem.Web.Controllers
{
    [Authorize]
    public class QuizzesController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.context.Categories.All().ToList();

            List<SelectListItem> categoriesFilterListItems = new List<SelectListItem>();
            categoriesFilterListItems.Add(new SelectListItem { Text = "All", Value = "", Selected = true });
            categoriesFilterListItems.AddRange(categories.Select(x => new SelectListItem { Value = x.Name, Text = x.Name }));

            this.ViewData.Add("categoriesFilter", categoriesFilterListItems);

            string userId = this.User.Identity.GetUserId();

            var activeQuizzes = this.context.Quizzes.All()
                .Where(x => x.State == QuizState.Active && 
                    !x.Results.Any(r => r.UserId == userId))
                .Select(ModelConvertor.QuizToViewModel);

            var dataPager = new SimpleDataPager<QuizViewModel>(activeQuizzes, 10)
                .Sort("PublishDate", SortingDirection.Descending)
                .Load();

            return View(dataPager);
        }

        [HttpGet]
        [ActionName("UpdateGrid")]
        [NoCache]
        [HandleAjaxError]
        public ActionResult GetQuzzesGird()
        {
            string userId = this.User.Identity.GetUserId();

            var activeQuizzes = this.context.Quizzes.All()
                .Where(x => x.State == QuizState.Active && 
                    !x.Results.Any(r => r.UserId == userId))
                .Select(ModelConvertor.QuizToViewModel);

            var dataPager = new SimpleDataPager<QuizViewModel>(activeQuizzes, 10)
                .ProcessUrlParameters(this.Request.QueryString)
                .Load();

            return PartialView("_GridView", dataPager);
        }

        [HttpGet]
        public ActionResult Solve(int quizId)
        {
            QuizSolveModel quiz = this.context.Quizzes.All()
                .Where(x => x.Id == quizId)
                .Select(ModelConvertor.QuizToSolveModel)
                .First();

            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Solve()
        {
            string userId = this.User.Identity.GetUserId();

            int quizId = int.Parse(this.Request.Form.Get("quizId"));

            QuizSolvedModel solvedQuiz =
                this.context.Quizzes.All()
                .Where(x => x.Id == quizId)
                .Select(ModelConvertor.QuizToSolvedModel).First();

            Dictionary<int,int> answeredQuestions =
                this.Request.Form.AllKeys
                .Where(x => Regex.IsMatch(x, @"^q-\d+$"))
                .ToDictionary(x => int.Parse(x.Substring(2)), x => int.Parse(this.Request.Form.Get(x)));

            for (int i = 0; i < solvedQuiz.Questions.Count; i++)
            {
                solvedQuiz.Questions[i].SelectedAnswerId = 
                    answeredQuestions[solvedQuiz.Questions[i].Id];

                if (solvedQuiz.Questions[i].RightAnswerId == solvedQuiz.Questions[i].SelectedAnswerId)
                {
                    solvedQuiz.Points++;
                }
            }

            this.ManageResults(userId, solvedQuiz);

            solvedQuiz.IsVotableByUser = 
                !this.context.Votes.All()
                .Any(x => x.QuizId == quizId && x.UserId == userId);

            return View("Solved", solvedQuiz);
        }

        [HttpPost]
        [HandleAjaxError]
        public ActionResult Vote(int quizId, int value)
        {
            string userId = this.User.Identity.GetUserId();

            if (userId == null)
            {
                throw new HttpException(400, "You are not authorized.");
            }

            if (this.context.Votes.All().Any(x => x.QuizId == quizId && x.UserId == userId))
            {
                throw new HttpException(400, "You already voted for this quiz.");
            }

            Quiz quiz = this.context.Quizzes.GetById(quizId);
            quiz.Rating += value;
            quiz.Votes.Add(new Vote { UserId = userId, Value = value });

            this.context.Quizzes.Update(quiz);
            this.context.SaveChanges();

            return Content(value.ToString());
        }

        public ActionResult Archive()
        {
            var categories = this.context.Categories.All().ToList();

            List<SelectListItem> categoriesFilterListItems = new List<SelectListItem>();
            categoriesFilterListItems.Add(new SelectListItem { Text = "All", Value = "", Selected = true });
            categoriesFilterListItems.AddRange(categories.Select(x => new SelectListItem { Value = x.Name, Text = x.Name }));

            this.ViewData.Add("categoriesFilter", categoriesFilterListItems);

            string userId = this.User.Identity.GetUserId();

            var activeQuizzes = this.context.Results.All()
                .Where(x => x.UserId == userId)
                .Select(ModelConvertor.ResultToQuizArchiveModel);

            var dataPager = new SimpleDataPager<QuizArchiveModel>(activeQuizzes, 10)
                .ProcessUrlParameters(this.Request.RequestType == "GET" ? this.Request.QueryString : this.Request.Form)
                .Load();

            return View(dataPager);
        }

        [NonAction]
        private void ManageResults(string userId, QuizSolvedModel solvedQuiz)
        {
            QuizResult result =
                this.context.Results.All()
                .FirstOrDefault(x => x.UserId == userId && x.QuizId == solvedQuiz.Id);

            if (result == null)
            {
                result = new QuizResult
                {
                    UserId = userId,
                    QuizId = solvedQuiz.Id,
                    FirstResult = solvedQuiz.CalculateResult(),
                };

                result.LastResult = result.FirstResult;

                this.context.Results.Add(result);
                this.context.SaveChanges();
            }
            else
            {
                result.LastResult = solvedQuiz.CalculateResult();
                this.context.Results.Update(result);
                this.context.SaveChanges();
            }
        }
	}
}